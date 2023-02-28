USE [Northwind]
GO

/****** Object:  Table [dbo].[users]    Script Date: 01/03/2023 03:24:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[users](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[user_name] [nvarchar](55) NULL,
	[user_password] [nvarchar](128) NULL,
	[user_salt] [uniqueidentifier] NULL,
	[rowguid] [uniqueidentifier] NULL,
	[modified_date] [datetime] NULL,
 CONSTRAINT [pk_user_id] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[user_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[users] ADD  DEFAULT (newid()) FOR [rowguid]
GO

ALTER TABLE [dbo].[users] ADD  DEFAULT (getdate()) FOR [modified_date]
GO


