/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

-- Survey questions merge
--:r .\dbo\PostDeploymentScripts\PopulateSurveyQuestions.sql

-- Teachers Aide course data merge
--:r .\dbo\PostDeploymentScripts\Courses\PopulateTeachersAidCourseData.sql

-- View Config data merge
--:r .\dbKUo\PostDeploymentScripts\ViewConfig\PopulateViewConfigDataForAllCourses.sql

-- Survey questions merge
--:r .\dbo\PostDeploymentScripts\Sales\PopulateSalesScripts.sql

-- Sales roles
--:r .\dbo\PostDeploymentScripts\Sales\PopulateSalesRoles.sql

-- Mobile content
--:r .\dbo\PostDeploymentScripts\Mobile\mobile_content.sql