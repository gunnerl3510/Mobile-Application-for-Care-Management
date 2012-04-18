CREATE PROCEDURE [dbo].[WriteToLog]
	@log_date		[datetime],
	@thread			[varchar](255),
	@log_level		[varchar](50),
	@logger			[varchar](255),
	@message		[varchar](4000),
	@exception		[varchar](2000)
AS
	INSERT INTO [dbo].[Log] (
		[Date],
		[Thread],
		[Level],
		[Logger],
		[Message],
		[Exception]
	)
	SELECT	@log_date, 
			@thread, 
			@log_level, 
			@logger, 
			@message, 
			@exception
RETURN 0