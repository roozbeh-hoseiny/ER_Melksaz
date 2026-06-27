CREATE TABLE [dbo].[Settings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationName] [varchar](100) NOT NULL,
	[Environment] [varchar](100) NOT NULL,
	[Key] [varchar](200) NOT NULL,
	[Value] [nvarchar](4000) NOT NULL,
	[Version] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[Settings_Get]    Script Date: 5/28/2026 8:48:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[Settings_Get]
	@AppName VARCHAR(100),
	@Version VARCHAR(100),
	@Env VARCHAR(100)
AS
BEGIN
	;WITH RankedSettings AS
	(
		SELECT 
			Id,
			ApplicationName,
			[Environment],
			[Key],
			[Value],
			ROW_NUMBER() OVER (
				PARTITION BY [Key]
				ORDER BY
					CASE WHEN ApplicationName = @AppName THEN 2 WHEN ApplicationName = '*' THEN 1 ELSE 0 END DESC,
					CASE WHEN [Environment] = @Env THEN 2 WHEN [Environment] = '*' THEN 1 ELSE 0 END DESC,
					CASE WHEN [Version] = @Version THEN 2 WHEN [Version] = '0.0.0.0' OR [Version] = '*' THEN 1 ELSE 0 END DESC
			) AS rn
		FROM [dbo].[Settings]
		WHERE
			(ApplicationName = @AppName OR ApplicationName = '*') AND
			([Environment] = @Env OR [Environment] = '*') AND
			([Version] = @Version OR [Version] = '*' OR [Version] = '0.0.0.0')
	)
	SELECT  
		Id,
		ApplicationName,
		[Environment],
		[Key],
		[Value]
	FROM RankedSettings
	WHERE rn = 1
END
GO

