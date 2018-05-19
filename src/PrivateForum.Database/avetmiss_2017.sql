CREATE PROCEDURE avetmiss_2017
AS

-- CLEAR ALL REPORT TABLES
delete from report_Avetmiss_ClientPostalDetails
delete from report_Avetmiss_Clients
delete from report_Avetmiss_CompletedPrograms
delete from report_Avetmiss_Disabilities
delete from report_Avetmiss_Enrolments
delete from report_Avetmiss_PriorEducationAchievements

declare @year int = 2017

-- DROP TABLE #completedClients
create table #completedClients
(
	Id int
)

INSERT INTO #completedClients (Id)
SELECT DISTINCT cp.ClientId FROM [dbo].[exam_CompletedPrograms] cp
INNER JOIN [dbo].[UsiInfos] ui ON cp.ClientId = ui.Id
INNER JOIN [dbo].[identity_Users] u ON u.Id = ui.Id AND u.Id NOT IN (2) 
INNER JOIN [dbo].[Clients] c ON u.Id = c.Id
WHERE cp.YearProgramCompleted = @year AND u.Id NOT IN (
--EXCLUDE THOSE THAT HAVE ISSUE WITH PREVIOUS YEAR
--815, 1208, 1219, 329,

--EXCLUDE THOSE THAT ARE NOTE FINISHED WITH EXAMS
--3057,

-- THIS PERSON COMPLETED PROGRAM 1 (DOES NOT GO TO COMPLETED PROGRAMS)
-- AND ALL HIS USEREXAMS ARE IN 2016
--1028,

-- THIS GUY IS INTERESTING, THINK WE ADDED WC TO HIM AND HE COMPLETED IN 2017
-- NO COMPLETED PROGRAM (ONLY 1,3)
--1108, 

3454,

3802,3733,3635,2320,1612,
3729,3710,3700,3650,3646,3643,3612,3565,3527,3169,3140,3087,2787,2767,2701,2624,2579,2444,2439,2378,2309,2179,2177,2148,2072,2065,1940,1934,1837,1648,1533,
3683,3661,3649,3621,3616,3584,3578,3539,3504,3488,3476,3446,3364,3357,3308,3304,3286,3243,3131,3092,3054,3011,3002,2939,2917,2907,2885,2870,2853,2848,2780,2779,2735,2683,2672,2668,2665,2647,2646,2621,2612,2569,2567,2557,2492,2442,2430,2427,2417,2367,2347,2304,2302,2286,2258,2249,2235,2225,2197,2175,2150,2149,2119,2097,2051,2024,2003,1987,1982,1976,1924,1647,1622,1463,1407,1275,1063
)


SELECT * FROM #completedClients







-- INSERT CLIENT POSTAL DETAILS
SET ANSI_WARNINGS  OFF
INSERT INTO report_Avetmiss_ClientPostalDetails
(ClientIdentifier, ClientTitle, ClientFirstName, ClientLastName, 
AddressName, 
AddressDetails, 
AddressStreetNumber, AddressStreetName, AddressPostalDeliveryBox, 
AddressPostal, Postcode, StateIdentifier, TelephoneNumberHome, TelephoneNumberWork, TelephoneNumberMobile, EmailAddress,         

ReportBatchNumber, Created, CreatedBy, Modified, ModifiedBy)

SELECT 
 c.Id, t.Code, u.FirstName, u.LastName, 
 CASE c.qas_addressline3 WHEN '' THEN CASE c.qas_addressline2 WHEN '' THEN c.qas_addressline1 ELSE c.qas_addressline2 END ELSE c.qas_addressline3 END,
 CASE WHEN c.[StreetName] IS NULL THEN CASE c.qas_addressline3 WHEN '' THEN CASE c.qas_addressline2 WHEN '' THEN c.qas_addressline1 ELSE c.qas_addressline2 END ELSE c.qas_addressline3 END ELSE c.[Address] END ,
 c.[StreetNumber], c.[StreetName], c.PostalDeliveryBox, 
 c.[qas_suburb], c.[qas_postcode], s.Code, REPLACE(u.ContactNumber, '+', ''), '', '', c.EmailAddress,
 
 0, getdate(), 'System', getdate(), 'System'
FROM [dbo].[Clients] c
INNER JOIN [dbo].[identity_Users] u ON u.Id = c.Id
INNER JOIN [dbo].[report_Avetmiss_Enum_StateType] s ON s.Id = c.StateType
INNER JOIN [dbo].[report_Avetmiss_Enum_TitleType] t ON t.Id = c.TitleType
INNER JOIN #completedClients cpc ON cpc.Id = c.Id
SELECT * FROM report_Avetmiss_ClientPostalDetails



-- INSERT CLIENTS
set ANSI_WARNINGS  OFF
INSERT INTO report_Avetmiss_Clients
(ClientIdentifier, NameForEncryption, HighestSchoolLevelCompletedIdentifier, 
YearHighestSchoolLevelCompleted, Sex, 
DateOfBirth, 
Postcode, IndigenousStatusIdentifier, LanguageIdentifier, LabourForceStatusIdentifier, CountryIdentifier, DisabilityFlag, PriorEducationAchievementFlag,
AtSchoolFlag, ProficiencyInSpokenEnglishIdentifier, AddressLocation, UniqueStudentIdentifier, StateIdentifier,

AddressName, 
AddressDetails, AddressStreetNumber,

AddressStreetName, StatisticalAreaLevel1Identifier, StatisticalAreaLevel2Identifier, 

ReportBatchNumber, Created, CreatedBy, Modified, ModifiedBy)


SELECT 
	c.Id, CAST(u.FirstName + ISNULL(' ' + u.MiddleName, '') + ' ' + u.LastName AS NVARCHAR), h.Code, 
	(CASE WHEN h.Code = '02' THEN '@@@@' ELSE CASE WHEN (c.YearHighestSchoolLevelCompleted) < (YEAR(CASE WHEN c.DateOfBirth < '1910-01-01' THEN '1970-01-20' WHEN c.DateOfBirth > '2015-01-01' THEN '1970-01-20' ELSE c.DateOfBirth END) + 5) 
		THEN CAST((YEAR(CASE WHEN c.DateOfBirth < '1910-01-01' THEN '1970-01-20' WHEN c.DateOfBirth > '2015-01-01' THEN '1970-01-20' ELSE c.DateOfBirth END) + 18) AS NVARCHAR) ELSE CAST(c.YearHighestSchoolLevelCompleted AS NVARCHAR) END END), c.Sex, 
	(CASE WHEN c.DateOfBirth < '1910-01-01' THEN '20011970' WHEN c.DateOfBirth > '2015-01-01' THEN '20011970' ELSE REPLACE(CONVERT(CHAR(10), c.DateOfBirth, 103), '/', '') END), 
	c.qas_postcode, i.Code, l.Code, la.Code, 
	CASE WHEN i.Code IN ('1', '2', '3') THEN '1101' ELSE co.Code END, 
	c.Disability, c.PriorEducationalAchievement, 
	c.AtSchool, p.Code, c.qas_suburb, ui.Usi, s.Code,
  
		CASE WHEN c.[StreetName] IS NULL 
			THEN 
				CASE c.qas_addressline3 WHEN '' 
					THEN 
						CASE c.qas_addressline2 WHEN '' 
							THEN c.qas_addressline1 
							ELSE c.qas_addressline2 
						END 
					ELSE c.qas_addressline3 
				END 
			ELSE c.[Address] 
		END
	
	, c.[Address]	
	, c.[StreetNumber] 
	, c.[StreetName]

	,c.StatisticalAreaLevel1, c.StatisticalAreaLevel2,
 
 0, getdate(), 'System', getdate(), 'System'
FROM [dbo].[Clients] c
INNER JOIN [dbo].[identity_Users] u ON u.Id = c.Id
INNER JOIN [dbo].[UsiInfos] ui ON u.Id = ui.Id
INNER JOIN #completedClients cpc ON cpc.Id = c.Id

INNER JOIN [dbo].[report_Avetmiss_Enum_StateType] s ON s.Id = c.StateType
INNER JOIN [dbo].[report_Avetmiss_Enum_HighestSchoolLevelCompletedType] h ON h.Id = CAST(c.HighestSchoolLevelCompleted AS INT)
INNER JOIN [dbo].[report_Avetmiss_Enum_IndigenousStatusType] i ON i.Id = c.IndigenousStatusType
INNER JOIN [dbo].[report_Avetmiss_Enum_LanguageType] l ON l.Id = CAST(c.Language AS INT)
INNER JOIN [dbo].[report_Avetmiss_Enum_LabourForceStatusType] la ON la.Id = CAST(c.LabourForceStatus AS INT)
INNER JOIN [dbo].[report_Avetmiss_Enum_CountryType] co ON co.Id = CAST(c.Country AS INT)
INNER JOIN [dbo].[report_Avetmiss_Enum_ProficiencyInSpokenEnglishType] p ON p.Id = CAST(c.ProficiencyInSpokenEnglish AS INT)

LEFT JOIN [dbo].[report_Avetmiss_ClientAddress] ca ON ca.ClientIdentifier = c.Id

SELECT * FROM report_Avetmiss_Clients







-- INSERT COMPLETED PROGRAMS
set ANSI_WARNINGS  OFF
INSERT INTO report_Avetmiss_CompletedPrograms
(TrainingOrganisationIdentifier, ProgramIdentifier, ClientIdentifier, [Year], IssuedFlag,

ReportBatchNumber, Created, CreatedBy, Modified, ModifiedBy)

SELECT DISTINCT
 '41283', 
 
-- program
	CASE c.ProgramId 
	WHEN 2 THEN CASE WHEN c.Created < '2017-12-09' THEN '39291QLD' ELSE '10274NAT' END 
	WHEN 4 THEN 'CHC30113' 
	WHEN 5 THEN 'CHC30213' 
	ELSE '' END,

 c.ClientId, c.YearProgramCompleted, 'Y',
 
 0, getdate(), 'System', getdate(), 'System'
FROM [dbo].[exam_CompletedPrograms] c
INNER JOIN #completedClients cpc ON cpc.Id = c.ClientId
WHERE c.YearProgramCompleted = @year AND c.ProgramId IN (2, 4, 5)

SELECT * FROM report_Avetmiss_CompletedPrograms


-- DISABILITIES
set ANSI_WARNINGS  OFF
INSERT INTO report_Avetmiss_Disabilities
(ClientIdentifier, DisabilityTypeIdentifier,

ReportBatchNumber, Created, CreatedBy, Modified, ModifiedBy)

SELECT DISTINCT
 d.ClientId, di.Code, 
 
 0, getdate(), 'System', getdate(), 'System'
FROM [dbo].[Disabilities] d
INNER JOIN #completedClients cpc ON cpc.Id = d.ClientId
INNER JOIN [dbo].[report_Avetmiss_Enum_DisabilityType] di ON di.Id = d.DisabilityType

SELECT * FROM report_Avetmiss_Disabilities


-- PRIOR EDUCATION ACHIVEMENTS
set ANSI_WARNINGS  OFF
INSERT INTO report_Avetmiss_PriorEducationAchievements
(ClientIdentifier, PriorEducationAchievementIdentifier,

ReportBatchNumber, Created, CreatedBy, Modified, ModifiedBy)

SELECT DISTINCT
 p.ClientId, pr.Code, 
 
 0, getdate(), 'System', getdate(), 'System'
FROM [dbo].[PriorEducationAchievements] p
INNER JOIN #completedClients cpc ON cpc.Id = p.ClientId
INNER JOIN [dbo].[report_Avetmiss_Enum_PriorEducationAchievementType] pr ON pr.Id = p.PriorEducationAchievementType

SELECT * FROM report_Avetmiss_PriorEducationAchievements


-- UPDATE CLIENT DISABILITY FLAG
UPDATE report_Avetmiss_Clients SET DisabilityFlag = 'N'
UPDATE a SET a.DisabilityFlag = 'Y'
FROM report_Avetmiss_Clients a
INNER JOIN 
(
	SELECT c.Id, COUNT(*) AS c
	FROM [dbo].[Disabilities] d
	INNER JOIN [dbo].[Clients] c ON d.ClientId = c.Id
	INNER JOIN #completedClients cpc ON cpc.Id = c.Id
	GROUP BY c.Id
) t ON a.ClientIdentifier = t.Id
WHERE t.c > 0


-- UPDATE CLIENT PRIOR EDUCATION ACHIEVEMENT FLAG
UPDATE report_Avetmiss_Clients SET PriorEducationAchievementFlag = 'N'
UPDATE a SET a.PriorEducationAchievementFlag = 'Y'
FROM report_Avetmiss_Clients a
INNER JOIN 
(
	SELECT c.Id, COUNT(*) AS c
	FROM [dbo].[PriorEducationAchievements] d
	INNER JOIN [dbo].[Clients] c ON d.ClientId = c.Id
	INNER JOIN #completedClients cpc ON cpc.Id = c.Id
	GROUP BY c.Id
) t ON a.ClientIdentifier = t.Id
WHERE t.c > 0






-- ENROLMENTS
set ANSI_WARNINGS  OFF
INSERT INTO report_Avetmiss_Enrolments
(TrainingOrganisationDeliveryLocationIdentifier, ClientIdentifier, 
SubjectIdentifier, 
ProgramIdentifier, 
ActivityStartDate,
ActivityEndDate, 
DeliveryModeIdentifier,
OutcomeIdentifier, ScheduledHours, FundingSource, CommencingProgramIdentifier, TrainingContractIdentifier, ApprenticeshipsClientIdentifier, StudyReasonIdentifier, 
VETSchoolsFlag, SpecificFundingIdentifier, TrainingOrganisationOutcomeIdentifier, StateTrainingAuthorityFundingSource, ClientTuitionFee, FeeTypeIdentifier,
PurchasingContractIdentifier, PurchasingContactScheduleIdentifier, HoursAttended, AssociatedCourseIdentifier,

ReportBatchNumber, Created, CreatedBy, Modified, ModifiedBy)



SELECT 
'1', dat.a, dat.b, dat.c, REPLACE(CONVERT(CHAR(10), MIN(dat.d), 103), '/', ''), REPLACE(CONVERT(CHAR(10), MAX(dat.e), 103), '/', ''),  
	'20',
	'20', dat.f, '20', dat.g, '', '', '@@', 
	'N', '', '', '', '', '',
	'', '', '', '',
	 0, getdate(), 'System', getdate(), 'System'

FROM 
(
	SELECT DISTINCT 

		ue.ClientId AS a

	-- subject
	,	CASE ue.ExamModuleId 
		WHEN 9  THEN 'CPCCCM1011A' 
		WHEN 10 THEN 'CPCCCM1013A' 
		WHEN 11 THEN 'CPCCCM1014A' 
		WHEN 12 THEN 'CPCCCM2001A' 
		WHEN 13 THEN 'CPCCOHS2001A'  

		WHEN 20 THEN CASE WHEN ue.Created < '2017-12-09' THEN 'CPCCOHS1001A' ELSE 'CPCCWHS1001' END 
		WHEN 21 THEN CASE WHEN ue.Created < '2017-12-09' THEN 'CPCCOHS1001A' ELSE 'CPCCWHS1001' END 
		WHEN 22 THEN CASE WHEN ue.Created < '2017-12-09' THEN 'CPCCOHS1001A' ELSE 'CPCCWHS1001' END 

		ELSE 'QLD330OBQ01A' END AS b

	-- program
	,	CASE ppm.ProgramId 
		WHEN 2 THEN CASE WHEN ue.Created < '2017-12-09' THEN '39291QLD' ELSE '10274NAT' END 
		WHEN 4 THEN 'CHC30113' 
		WHEN 5 THEN 'CHC30213' 
		ELSE '' END AS c
	, CASE WHEN YEAR(ue.DateStarted) < 1900 THEN ue.Created ELSE ue.DateStarted END AS d
	, ue.DateEnded AS e
	, CASE ue.ExamModuleId WHEN 20 THEN '6' WHEN 21 THEN '6' WHEN 22 THEN '6' ELSE '4' END AS f
	, CASE PPM.ProgramId WHEN 2 THEN '3' ELSE '8' END AS g

	FROM [dbo].[exam_UserExams] ue
	INNER JOIN [dbo].[Clients] c ON c.Id = ue.ClientId
	INNER JOIN #completedClients cpc ON cpc.Id = c.Id
	INNER JOIN [dbo].[exam_ExamModules] em ON em.Id = ue.ExamModuleId
	INNER JOIN [dbo].[exam_ProgramModules] PM ON PM.Id = em.ProgramModuleId
	INNER JOIN [dbo].[exam_ProgramProgramModules] PPM ON PPM.ProgramModuleId = PM.Id
	WHERE  em.Id NOT IN (23, 24, 115, 116) 
	AND YEAR(CASE WHEN YEAR(ue.DateStarted) < 1900 THEN ue.Modified ELSE ue.DateStarted END) = @year 
	AND YEAR(ue.DateEnded) = @year
) AS dat
GROUP BY dat.a, dat.b, dat.c, dat.f, dat.g
ORDER BY dat.a



SELECT * FROM report_Avetmiss_Enrolments

DROP TABLE #completedClients	


SELECT 'COMPARE TO PREVIOUS YEAR'


--SELECT * FROM [dbo].[report_Avetmiss_ClientPostalDetails] new_year
--	INNER JOIN [dbo].[report_Avetmiss_ClientPostalDetails_2016] old_year ON new_year.ClientIdentifier = old_year.ClientIdentifier

--SELECT * FROM [dbo].[report_Avetmiss_Clients] new_year
--	INNER JOIN [dbo].[report_Avetmiss_Clients_2016] old_year ON new_year.ClientIdentifier = old_year.ClientIdentifier

--SELECT * FROM [dbo].[report_Avetmiss_Disabilities] new_year
--	INNER JOIN [dbo].[report_Avetmiss_Disabilities_2016] old_year ON new_year.ClientIdentifier = old_year.ClientIdentifier
	
--SELECT * FROM [dbo].[report_Avetmiss_PriorEducationAchievements] new_year
--	INNER JOIN [dbo].[report_Avetmiss_PriorEducationAchievements_2016] old_year ON new_year.ClientIdentifier = old_year.ClientIdentifier
	

--SELECT * FROM [dbo].[report_Avetmiss_CompletedPrograms] new_year
--	INNER JOIN [dbo].[report_Avetmiss_CompletedPrograms_2016] old_year ON new_year.TrainingOrganisationIdentifier = old_year.TrainingOrganisationIdentifier
--																	   AND  new_year.ClientIdentifier = old_year.ClientIdentifier

--SELECT * FROM [dbo].[report_Avetmiss_Enrolments] new_year
--	INNER JOIN [dbo].[report_Avetmiss_Enrolments_2016] old_year ON new_year.ClientIdentifier = old_year.ClientIdentifier
--																	   AND  new_year.SubjectIdentifier = old_year.SubjectIdentifier


SELECT 'CHECK ALL CLIENTS'

--SELECT * FROM [dbo].[report_Avetmiss_Clients]
--	WHERE ClientIdentifier NOT IN (SELECT ClientIdentifier FROM [dbo].[report_Avetmiss_Enrolments])

	
--GO
--SELECT * FROM [dbo].[report_Avetmiss_Clients]
--	WHERE ClientIdentifier NOT IN (SELECT ClientIdentifier FROM [dbo].[report_Avetmiss_Enrolments])

--GO
--declare @userid INT = 329                                    
--SELECT * FROM [DBO].[identity_Users] WHERE Id = @userid
--SELECT * FROM [DBO].[Clients] WHERE Id = @userid
--SELECT * FROM [dbo].[report_Avetmiss_Clients] WHERE ClientIdentifier = @userid
--SELECT * FROM [dbo].[report_Avetmiss_ClientPostalDetails] WHERE ClientIdentifier = @userid

 
--GO
--declare @userid INT = 329

--SELECT * FROM [dbo].[exam_CompletedPrograms] WHERE ClientId = @userid
--SELECT * FROM [dbo].[exam_UserExams] WHERE ClientId = @userid
--SELECT * FROM [dbo].[report_Avetmiss_Enrolments] where ClientIdentifier = @userid
--SELECT * FROM [dbo].[report_Avetmiss_CompletedPrograms] where ClientIdentifier = @userid



SELECT 'AVETMISS QUERIES'

--SELECT * FROM [dbo].[report_Avetmiss]
--SELECT * FROM [dbo].[report_Avetmiss_ClientPostalDetails]
--SELECT * FROM [dbo].[report_Avetmiss_Clients]
--SELECT * FROM [dbo].[report_Avetmiss_CompletedPrograms]
--SELECT * FROM [dbo].[report_Avetmiss_Disabilities]
--SELECT * FROM [dbo].[report_Avetmiss_Enrolments]
--SELECT * FROM [dbo].[report_Avetmiss_Enum_CountryType]
--SELECT * FROM [dbo].[report_Avetmiss_Enum_DisabilityType]
--SELECT * FROM [dbo].[report_Avetmiss_Enum_EmploymentStatusType]
--SELECT * FROM [dbo].[report_Avetmiss_Enum_HighestSchoolLevelCompletedType]
--SELECT * FROM [dbo].[report_Avetmiss_Enum_IndigenousStatusType]
--SELECT * FROM [dbo].[report_Avetmiss_Enum_LabourForceStatusType]
--SELECT * FROM [dbo].[report_Avetmiss_Enum_LanguageType]
--SELECT * FROM [dbo].[report_Avetmiss_Enum_PriorEducationAchievementType]
--SELECT * FROM [dbo].[report_Avetmiss_Enum_ProficiencyInSpokenEnglishType]
--SELECT * FROM [dbo].[report_Avetmiss_Enum_StateType]
--SELECT * FROM [dbo].[report_Avetmiss_Enum_StudyReasonType]
--SELECT * FROM [dbo].[report_Avetmiss_Enum_TitleType]
--SELECT * FROM [dbo].[report_Avetmiss_ManagingAgents]
--SELECT * FROM [dbo].[report_Avetmiss_PriorEducationAchievements]
--SELECT * FROM [dbo].[report_Avetmiss_TrainingOrganisationDeliveryLocations]
--SELECT * FROM [dbo].[report_Avetmiss_TrainingOrganisations]


--SELECT * FROM [dbo].[report_Avetmiss_Programs]
--SELECT * FROM [dbo].[report_Avetmiss_Subjects]
