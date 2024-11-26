USE [ekozig_basic_form]
GO
/****** Object:  User [user]    Script Date: 2024. 11. 26. 10:55:30 ******/
CREATE USER [user] FOR LOGIN [user] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 2024. 11. 26. 10:55:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[AddressID] [int] IDENTITY(1,1) NOT NULL,
	[PostCode] [nvarchar](4) NOT NULL,
	[Town] [nvarchar](100) NOT NULL,
	[Street] [nvarchar](100) NOT NULL,
	[StreetType] [nvarchar](50) NOT NULL,
	[HouseNumber] [int] NOT NULL,
	[Floor] [int] NULL,
	[Door] [int] NULL,
	[RingNumber] [int] NULL,
	[created_at] [datetime] NULL,
	[modified_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[AddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Entry]    Script Date: 2024. 11. 26. 10:55:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entry](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[AddressID] [int] NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Phone] [nvarchar](50) NULL,
	[Sex] [varchar](10) NOT NULL,
	[created_at] [datetime] NULL,
	[modified_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Address] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[Address] ADD  DEFAULT (getdate()) FOR [modified_at]
GO
ALTER TABLE [dbo].[Entry] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[Entry] ADD  DEFAULT (getdate()) FOR [modified_at]
GO
ALTER TABLE [dbo].[Entry]  WITH CHECK ADD  CONSTRAINT [FK_entry_Address] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Address] ([AddressID])
GO
ALTER TABLE [dbo].[Entry] CHECK CONSTRAINT [FK_entry_Address]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [CK_StreetType] CHECK  (([StreetType]='Park' OR [StreetType]='Drive' OR [StreetType]='Road' OR [StreetType]='Lane' OR [StreetType]='Boulevard' OR [StreetType]='Avenue' OR [StreetType]='Street'))
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [CK_StreetType]
GO
ALTER TABLE [dbo].[Entry]  WITH CHECK ADD  CONSTRAINT [CK_Sex] CHECK  (([Sex]='female' OR [Sex]='male'))
GO
ALTER TABLE [dbo].[Entry] CHECK CONSTRAINT [CK_Sex]
GO
