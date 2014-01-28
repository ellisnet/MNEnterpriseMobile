use DATABASE_NAME_HERE;
go

--create our MvxDemo schema that all of our
--  MvvmCross database objects will belong to
create schema MvxDemo;
go

--create the user account that our application will use to
--  connect to the database
--  (Might have to delete existing MvvmCrossDemo user)
create login MvvmCrossDemo with password = 'MvxCr0s5Demo$1';
go
create user MvvmCrossDemo for login MvvmCrossDemo
	with default_schema = MvxDemo;
go

--create a table for the users
create table [MvxDemo].[Users] (
	[ID] int IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
	[FirstName] varchar(100),
	[LastName] varchar(100),
	[EmailAddress] varchar(255),
	[Password] varchar(30)
);
go

--add some users
insert into [MvxDemo].[Users]
	(FirstName, LastName, EmailAddress, [Password])
values
	('Fred', 'Johnson', 'fred@mvxdemo.com', 'Test1');

insert into [MvxDemo].[Users]
	(FirstName, LastName, EmailAddress, [Password])
values
	('Sally', 'Anderson', 'sally@mvxdemo.com', 'Test1');

insert into [MvxDemo].[Users]
	(FirstName, LastName, EmailAddress, [Password])
values
	('Tom', 'Smith', 'tom@mvxdemo.com', 'Test1');

insert into [MvxDemo].[Users]
	(FirstName, LastName, EmailAddress, [Password])
values
	('Jane', 'Roberts', 'jane@mvxdemo.com', 'Test1');
go

--create a table for our survey questions
create table [MvxDemo].[Questions] (
	[ID] int IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
	[QuestionOrder] int NOT NULL,
	[QuestionText] varchar(500)
);
go

--create a table for answer choices for the questions
create table [MvxDemo].[Answers] (
	[ID] int IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
	[AnswerOrder] int NOT NULL,
	[QuestionID] int NOT NULL,
	[AnswerText] varchar(100)
);
go

--add some questions and answer choices
declare @CurrentQuestionID int;

--Question 1
insert into [MvxDemo].[Questions]
	(QuestionOrder, QuestionText)
values (1, 'How are you feeling today?');

set @CurrentQuestionID = SCOPE_IDENTITY();

insert into [MvxDemo].[Answers]
	(AnswerOrder, QuestionID, AnswerText)
values (1, @CurrentQuestionID, 'Very bad');
insert into [MvxDemo].[Answers]
	(AnswerOrder, QuestionID, AnswerText)
values (2, @CurrentQuestionID, 'Bad');
insert into [MvxDemo].[Answers]
	(AnswerOrder, QuestionID, AnswerText)
values (3, @CurrentQuestionID, 'Neutral');
insert into [MvxDemo].[Answers]
	(AnswerOrder, QuestionID, AnswerText)
values (4, @CurrentQuestionID, 'Good');
insert into [MvxDemo].[Answers]
	(AnswerOrder, QuestionID, AnswerText)
values (5, @CurrentQuestionID, 'Very good');

--Question 2
insert into [MvxDemo].[Questions]
	(QuestionOrder, QuestionText)
values (2, 'How did you feel yesterday?');

set @CurrentQuestionID = SCOPE_IDENTITY();

insert into [MvxDemo].[Answers]
	(AnswerOrder, QuestionID, AnswerText)
values (1, @CurrentQuestionID, 'Very bad');
insert into [MvxDemo].[Answers]
	(AnswerOrder, QuestionID, AnswerText)
values (2, @CurrentQuestionID, 'Bad');
insert into [MvxDemo].[Answers]
	(AnswerOrder, QuestionID, AnswerText)
values (3, @CurrentQuestionID, 'Neutral');
insert into [MvxDemo].[Answers]
	(AnswerOrder, QuestionID, AnswerText)
values (4, @CurrentQuestionID, 'Good');
insert into [MvxDemo].[Answers]
	(AnswerOrder, QuestionID, AnswerText)
values (5, @CurrentQuestionID, 'Very good');

--Question 3
insert into [MvxDemo].[Questions]
	(QuestionOrder, QuestionText)
values (3, 'How would you describe your outlook about the future?');

set @CurrentQuestionID = SCOPE_IDENTITY();

insert into [MvxDemo].[Answers]
	(AnswerOrder, QuestionID, AnswerText)
values (1, @CurrentQuestionID, 'Very negative');
insert into [MvxDemo].[Answers]
	(AnswerOrder, QuestionID, AnswerText)
values (2, @CurrentQuestionID, 'Negative');
insert into [MvxDemo].[Answers]
	(AnswerOrder, QuestionID, AnswerText)
values (3, @CurrentQuestionID, 'Neutral');
insert into [MvxDemo].[Answers]
	(AnswerOrder, QuestionID, AnswerText)
values (4, @CurrentQuestionID, 'Positive');
insert into [MvxDemo].[Answers]
	(AnswerOrder, QuestionID, AnswerText)
values (5, @CurrentQuestionID, 'Very positive');

--Question 4
insert into [MvxDemo].[Questions]
	(QuestionOrder, QuestionText)
values (4, 'How would you rate the pain associated with moving your legs?');

set @CurrentQuestionID = SCOPE_IDENTITY();

insert into [MvxDemo].[Answers]
	(AnswerOrder, QuestionID, AnswerText)
values (1, @CurrentQuestionID, 'Extremely painful');
insert into [MvxDemo].[Answers]
	(AnswerOrder, QuestionID, AnswerText)
values (2, @CurrentQuestionID, 'Very painful');
insert into [MvxDemo].[Answers]
	(AnswerOrder, QuestionID, AnswerText)
values (3, @CurrentQuestionID, 'Normal pain level');
insert into [MvxDemo].[Answers]
	(AnswerOrder, QuestionID, AnswerText)
values (4, @CurrentQuestionID, 'Very little pain');
insert into [MvxDemo].[Answers]
	(AnswerOrder, QuestionID, AnswerText)
values (5, @CurrentQuestionID, 'No pain');

--Question 5
insert into [MvxDemo].[Questions]
	(QuestionOrder, QuestionText)
values (5, 'How would you rate the pain associated with moving your arms?');

set @CurrentQuestionID = SCOPE_IDENTITY();

insert into [MvxDemo].[Answers]
	(AnswerOrder, QuestionID, AnswerText)
values (1, @CurrentQuestionID, 'Extremely painful');
insert into [MvxDemo].[Answers]
	(AnswerOrder, QuestionID, AnswerText)
values (2, @CurrentQuestionID, 'Very painful');
insert into [MvxDemo].[Answers]
	(AnswerOrder, QuestionID, AnswerText)
values (3, @CurrentQuestionID, 'Normal pain level');
insert into [MvxDemo].[Answers]
	(AnswerOrder, QuestionID, AnswerText)
values (4, @CurrentQuestionID, 'Very little pain');
insert into [MvxDemo].[Answers]
	(AnswerOrder, QuestionID, AnswerText)
values (5, @CurrentQuestionID, 'No pain');

--Question 6
insert into [MvxDemo].[Questions]
	(QuestionOrder, QuestionText)
values (6, 'How would you rate your ability to cope with day-to-day activities?');

set @CurrentQuestionID = SCOPE_IDENTITY();

insert into [MvxDemo].[Answers]
	(AnswerOrder, QuestionID, AnswerText)
values (1, @CurrentQuestionID, 'Very poor');
insert into [MvxDemo].[Answers]
	(AnswerOrder, QuestionID, AnswerText)
values (2, @CurrentQuestionID, 'Poor');
insert into [MvxDemo].[Answers]
	(AnswerOrder, QuestionID, AnswerText)
values (3, @CurrentQuestionID, 'Fair');
insert into [MvxDemo].[Answers]
	(AnswerOrder, QuestionID, AnswerText)
values (4, @CurrentQuestionID, 'Good');
insert into [MvxDemo].[Answers]
	(AnswerOrder, QuestionID, AnswerText)
values (5, @CurrentQuestionID, 'Very good');

go

--create a table for our users' answers
create table [MvxDemo].[UserAnswers] (
	[ID] int IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
	[Timestamp] datetime NOT NULL default getdate(),
	[UserID] int,
	[QuestionID] int,
	[AnswerID] int
);
go

--create the stored procedure for checking to see
--  if a user has logged in properly
create procedure [MvxDemo].[LoginUser]
	@EmailAddress varchar(255),
	@Password varchar(30)
as
begin
	select top 1 [ID], FirstName, LastName 
		from [MvxDemo].[Users]
		where EmailAddress = @EmailAddress
		and [Password] = @Password;
end
go
grant execute on [MvxDemo].[LoginUser]
	to MvvmCrossDemo;
go

--create the stored procedure for getting the questions and 
--  possible answers
create procedure [MvxDemo].[GetQuestionsAndAnswers]
as
begin
	select * from [MvxDemo].[Questions] order by QuestionOrder;
	select * from [MvxDemo].[Answers]
		where QuestionID in 
		(select [ID] from [MvxDemo].[Questions])
		order by AnswerOrder;
end
go
grant execute on [MvxDemo].[GetQuestionsAndAnswers]
	to MvvmCrossDemo;
go

--create the stored procedure for saving user answers to questions
create procedure [MvxDemo].[SaveUserAnswer]
	@UserID int,
	@QuestionID int,
	@AnswerID int
as
begin
	insert into [MvxDemo].[UserAnswers]
		(UserID, QuestionID, AnswerID)
	values
		(@UserID, @QuestionID, @AnswerID);
end
go
grant execute on [MvxDemo].[SaveUserAnswer]
	to MvvmCrossDemo;
go

--get my connection string
select
	'Server=' + @@servername + ';' 
	+ 'Database=' + db_name() + ';' 
	+ 'User Id=MvvmCrossDemo;' --using the user that was created above
	+ 'Password=MvxCr0s5Demo$1;'  --password configured above
as 'Database Connection String'
go

select 
	ua.UserID,
	u.FirstName,
	u.LastName,
	q.QuestionOrder,
	a.AnswerText, 
	ua.*
from MvxDemo.UserAnswers ua
	inner join MvxDemo.Users u on u.ID = ua.UserID
	inner join MvxDemo.Questions q on q.ID = ua.QuestionID
	inner join MvxDemo.Answers a on a.ID = ua.AnswerID
order by ua.Timestamp
go

/*
--remove all of the objects we created from the database
drop procedure [MvxDemo].[SaveUserAnswer];
drop procedure [MvxDemo].[GetQuestionsAndAnswers];
drop procedure [MvxDemo].[LoginUser];
drop table [MvxDemo].[UserAnswers];
drop table [MvxDemo].[Answers];
drop table [MvxDemo].[Questions];
drop table [MvxDemo].[Users];
drop user MvvmCrossDemo;
drop schema MvxDemo;
drop login MvvmCrossDemo;
go
*/
