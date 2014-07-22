use DATABASE_NAME_HERE;
go


--create the user account that our application will use to
--  connect to the database
--  (Might have to delete existing NewEmployeeDemo user)
create login NewEmployeeDemo with password = 'N3wEmploy33Demo$1';
go
create user NewEmployeeDemo for login NewEmployeeDemo
	with default_schema = dbo;
go

--get my connection string
select
	'Server=' + @@servername + ';' 
	+ 'Database=' + db_name() + ';' 
	+ 'User Id=NewEmployeeDemo;' --using the user that was created above
	+ 'Password=N3wEmploy33Demo$1;'  --password configured above
as 'Database Connection String'
go

--create a table for the Employees
create table [dbo].[Employees] (
	[EmployeeID] int IDENTITY(4001,1) NOT NULL PRIMARY KEY CLUSTERED,
	[Name] varchar(100),
	[EmailAddress] varchar(255)
);
go

--add some Employees
insert into [dbo].[Employees]
	(Name, EmailAddress)
values
	('Fred Johnson', 'fred@newemployeedemo.com');

insert into [dbo].[Employees]
	(Name, EmailAddress)
values
	('Sally Anderson', 'sally@newemployeedemo.com');

insert into [dbo].[Employees]
	(Name, EmailAddress)
values
	('Tom Smith', 'tom@newemployeedemo.com');

insert into [dbo].[Employees]
	(Name, EmailAddress)
values
	('Jane Roberts', 'jane@newemployeedemo.com');
go

--create the stored procedure for getting
--  employee details by employee id
create procedure [dbo].[LoginEmployee]
	@EmployeeID int
as
begin
	select top 1 EmployeeID, Name, EmailAddress
		from [dbo].[Employees]
		where EmployeeID = @EmployeeID;
end
go
grant execute on [dbo].[LoginEmployee]
	to NewEmployeeDemo;
go

select * from [dbo].[Employees];
go

--create a table for our documents that new employees
--  must review
create table [dbo].[Documents] (
	[DocumentID] int IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
	[Title] varchar(500) NOT NULL,
	[File] varchar(500) NOT NULL
);
go

insert into [dbo].[Documents]
	(Title, [File])
values
	('Employee Policies', 'Company_Employee_Policies.pdf');

insert into [dbo].[Documents]
	(Title, [File])
values
	('HIPAA Compliance Basics', 'HIPAA_Compliance_101.pdf');

insert into [dbo].[Documents]
	(Title, [File])
values
	('HIPAA Policies and Procedures', 'HIPAA_Policies_and_Procedures.pdf');

insert into [dbo].[Documents]
	(Title, [File])
values
	('Company Safety Protocols', 'Safety_Protocols.pdf');

insert into [dbo].[Documents]
	(Title, [File])
values
	('Sarbanes-Oxley Compliance Basics', 'Sarbanes-Oxley_Compliance_101.pdf');

insert into [dbo].[Documents]
	(Title, [File])
values
	('SOX Compliance Protocols', 'Sarbanes-Oxley_Policies_and_Procedures.pdf');
go

--create the stored procedure for getting
--  documents
create procedure [dbo].[GetDocument]
	@DocumentID int = NULL
as
begin --procedure
	if @DocumentID is null
	begin --get all documents
		select DocumentID, Title, [File]
			from [dbo].[Documents];
	end
	else
	begin --get the requested document
		select DocumentID, Title, [File]
			from [dbo].[Documents]
			where DocumentID = @DocumentID;
	end
end
go
grant execute on [dbo].[GetDocument]
	to NewEmployeeDemo;
go

select * from [dbo].[Documents];
go

--create a table for our tests that new employees
--  must take
create table [dbo].[Tests] (
	[TestID] int IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
	[Title] varchar(100) NOT NULL,
	[Description] varchar(500) NOT NULL,
	[PassingScore] int NOT NULL
);
go

insert into [dbo].[Tests]
	(Title, [Description], PassingScore)
values
	('Company Standards Quiz', 'Tests your knowledge of our company standards', 80);
go

insert into [dbo].[Tests]
	(Title, [Description], PassingScore)
values
	('HIPAA Compliance', 'Tests your knowledge of policies and procedures which ensure HIPAA compliance', 85);
go

insert into [dbo].[Tests]
	(Title, [Description], PassingScore)
values
	('SOX Compliance', 'Tests your knowledge of Sarbanes-Oxley compliance standards', 80);
go

insert into [dbo].[Tests]
	(Title, [Description], PassingScore)
values
	('Safety Protocols Quiz', 'Make sure you can help maintain a safe and OSHA-compliant work environment', 80);
go

--create the stored procedure for getting
--  a list of all Tests
create procedure [dbo].[GetTests]
as
begin --procedure
	select TestID, Title, [Description]
		from [dbo].[Tests]
end
go
grant execute on [dbo].[GetTests]
	to NewEmployeeDemo;
go

select * from [dbo].[Tests];
go

--create a table for our test questions
create table [dbo].[Questions] (
	[QuestionID] int IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
	[TestID] int NOT NULL,
	[QuestionOrder] int NOT NULL,
	[QuestionText] varchar(500) NOT NULL
);
go

--create a table for answer choices for the questions
create table [dbo].[Answers] (
	[AnswerID] int IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
	[QuestionID] int NOT NULL,
	[AnswerOrder] int NOT NULL,
	[AnswerText] varchar(100) NOT NULL,
	[CorrectAnswer] bit NOT NULL
);
go

--add some questions and answer choices
declare @CurrentTestID int = 
	(select TestID from [dbo].[Tests] where Title = 'HIPAA Compliance');
declare @CurrentQuestionID int;

insert into [dbo].[Questions]
	(TestID, QuestionOrder, QuestionText)
values
	(@CurrentTestID, 1, 'Is it okay to share protected health information (PHI) with friends?');

set @CurrentQuestionID = SCOPE_IDENTITY();

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 1, 'Always', 0);

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 2, 'Sometimes', 0);

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 3, 'It depends on whether they are a good friend', 0);

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 4, 'Never', 1);

insert into [dbo].[Questions]
	(TestID, QuestionOrder, QuestionText)
values
	(@CurrentTestID, 2, 'Should you let other users use your username and password?');

set @CurrentQuestionID = SCOPE_IDENTITY();

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 1, 'Always', 0);

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 2, 'Sometimes', 0);

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 3, 'Only if they really need to', 0);

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 4, 'Never', 1);

insert into [dbo].[Questions]
	(TestID, QuestionOrder, QuestionText)
values
	(@CurrentTestID, 3, 'Should you leave protected health information (PHI) laying around?');

set @CurrentQuestionID = SCOPE_IDENTITY();

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 1, 'Always', 0);

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 2, 'Sometimes', 0);

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 3, 'Only if it contains no embarrassing details', 0);

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 4, 'Never', 1);

insert into [dbo].[Questions]
	(TestID, QuestionOrder, QuestionText)
values
	(@CurrentTestID, 4, 'Should you make sure that only authorized users can access PHI?');

set @CurrentQuestionID = SCOPE_IDENTITY();

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 1, 'Always', 1);

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 2, 'Sometimes', 0);

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 3, 'Assume everyone is authorized', 0);

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 4, 'Never', 0);

insert into [dbo].[Questions]
	(TestID, QuestionOrder, QuestionText)
values
	(@CurrentTestID, 5, 'Is it okay to bring PHI home and show to family members?');

set @CurrentQuestionID = SCOPE_IDENTITY();

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 1, 'Always', 0);

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 2, 'Sometimes', 0);

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 3, 'Only after the kids have gone to bed', 0);

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 4, 'Never', 1);

insert into [dbo].[Questions]
	(TestID, QuestionOrder, QuestionText)
values
	(@CurrentTestID, 6, 'Should you lock your workstation when you go to lunch?');

set @CurrentQuestionID = SCOPE_IDENTITY();

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 1, 'Always', 1);

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 2, 'Sometimes', 0);

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 3, 'It depends on whether my coworkers are in the office', 0);

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 4, 'Never', 0);

insert into [dbo].[Questions]
	(TestID, QuestionOrder, QuestionText)
values
	(@CurrentTestID, 7, 'If health data can be used to identify a particular patient, is it PHI?');

set @CurrentQuestionID = SCOPE_IDENTITY();

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 1, 'Always', 1);

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 2, 'Sometimes', 0);

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 3, 'Only if the data is less than five years old', 0);

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 4, 'Never', 0);

insert into [dbo].[Questions]
	(TestID, QuestionOrder, QuestionText)
values
	(@CurrentTestID, 8, 'Should users be able to access PHI that they do not need for their job?');

set @CurrentQuestionID = SCOPE_IDENTITY();

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 1, 'Always', 0);

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 2, 'Sometimes', 0);

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 3, 'If they are an administrator or manager', 0);

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 4, 'Never', 1);

insert into [dbo].[Questions]
	(TestID, QuestionOrder, QuestionText)
values
	(@CurrentTestID, 9, 'HIPAA requires that patient data is protected from unauthorized access:');

set @CurrentQuestionID = SCOPE_IDENTITY();

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 1, 'Always', 1);

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 2, 'Sometimes', 0);

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 3, 'Unless it is marked non-confidential', 0);

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 4, 'Never', 0);

insert into [dbo].[Questions]
	(TestID, QuestionOrder, QuestionText)
values
	(@CurrentTestID, 10, 'HIPAA requires that computer security precautions are taken to protect patient data:');

set @CurrentQuestionID = SCOPE_IDENTITY();

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 1, 'Always', 1);

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 2, 'Sometimes', 0);

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 3, 'Except when installing software patches', 0);

insert into [dbo].[Answers]
	(QuestionID, AnswerOrder, AnswerText, CorrectAnswer)
values
	(@CurrentQuestionID, 4, 'Never', 0);
go

--stored procedure for getting test details
create procedure [dbo].[GetTestDetails]
	@TestID int
as
begin --procedure
	select TestID, Title, [Description], PassingScore
		from [dbo].[Tests]
		where TestID = @TestID;

	select QuestionID, QuestionOrder, QuestionText
		from [dbo].[Questions]
		where TestID = @TestID
		order by QuestionOrder;

	select a.AnswerID, a.QuestionID, a.AnswerOrder, a.AnswerText,
		a.CorrectAnswer, q.QuestionOrder
		from [dbo].[Answers] a
		inner join [dbo].[Questions] q on a.QuestionID = q.QuestionID
		where q.TestID = @TestID
		order by q.QuestionOrder, a.AnswerOrder;
end
go
grant execute on [dbo].[GetTestDetails]
	to NewEmployeeDemo;
go

declare @CurrentTestID int = 
	(select TestID from [dbo].[Tests] where Title = 'HIPAA Compliance');

exec [dbo].[GetTestDetails] @CurrentTestID;
go

--create a table for employee test scores
create table [dbo].[TestScores] (
	[TestScoreID] int IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
	[EmployeeID] int NOT NULL,
	[TestID] int NOT NULL,
	[Score] int NOT NULL,
	[Pass] bit NOT NULL,
	[TestTimestamp] datetime NOT NULL
);
go
--create the stored procedure for saving the scores
-- of tests that employees have taken
create procedure [dbo].[SaveTestScore]
	@EmployeeID int,
	@TestID int,
	@Score int,
	@Pass bit
as
begin
	insert into [dbo].[TestScores]
		(EmployeeID, TestID, Score, Pass, TestTimestamp)
	values
		(@EmployeeID, @TestID, @Score, @Pass, getdate());
end
go
grant execute on [dbo].[SaveTestScore]
	to NewEmployeeDemo;
go
