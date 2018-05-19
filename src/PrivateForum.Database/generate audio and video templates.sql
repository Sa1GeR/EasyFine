INSERT [dbo].[exam_DefaultTemplates] ([Id], [Name], [Template], [Type], [Content], [Answers], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (17, N'VIDEO RESPONSE', N'<div class="exam-questions-block-main">
    <div class="question1-block-main-eq">
        <div class="ques1-block-question-eq">
            <div class="ques1-block-questi-number-eq">Q{{questionNumber}}:</div>
            <div class="ques1-block-questi-text-eq">{{question}}</div>
            <div class="clearfix"></div>
        </div>
        <div>
            <div ng-bind="content[''description'']"/>
            <div>
                Press the button below to start recording.                
            </div>
			<div>
				Upload a file <input type="file" />
			</div>
			<div ng-if="isRecordingEnabled">		
				Or record yourself now			
				<div>
					<button type="button" ng-if="!isRecording" ng-click="startRecording()">Record</button>
					<button type="button" ng-if="isRecording" ng-click="stopRecording()">Stop</button>
				</div>	
			</div>			
			<div id="mediaContainer">
			</div>
        </div>
    </div>
</div>', N'17', N'{}', N'{}', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[exam_DefaultTemplates] ([Id], [Name], [Template], [Type], [Content], [Answers], [Created], [Modified], [CreatedBy], [ModifiedBy]) VALUES (18, N'AUDIO RESPONSE', N'<div class="exam-questions-block-main">
    <div class="question1-block-main-eq">
        <div class="ques1-block-question-eq">
            <div class="ques1-block-questi-number-eq">Q{{questionNumber}}:</div>
            <div class="ques1-block-questi-text-eq">{{question}}</div>
            <div class="clearfix"></div>
        </div>
        <div>
            <div ng-bind="content[''description'']"/>
            <div>
                Press the button below to start recording.                
            </div>
			<div>
				Upload a file <input type="file" />
			</div>
			<div ng-if="isRecordingEnabled">		
				Or record yourself now
				<div>
					<button type="button" ng-if="!isRecording" ng-click="startRecording()">Record</button>
					<button type="button" ng-if="isRecording" ng-click="stopRecording()">Stop</button>
				</div>
			</div>		
			<div id="mediaContainer">
			</div>
        </div>
    </div>
</div>', N'18', N'{}', N'{}', NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[exam_DefaultTemplates] OFF
GO
