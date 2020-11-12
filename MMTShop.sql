USE [MMTShop]
GO
/****** Object:  Schema [MMTShop]    Script Date: 11/12/2020 1:20:23 PM ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'MMTShop')
EXEC sys.sp_executesql N'CREATE SCHEMA [MMTShop]'
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 11/12/2020 1:20:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Categories]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](300) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[CategorySku]    Script Date: 11/12/2020 1:20:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CategorySku]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CategorySku](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SKURange] [nvarchar](2) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[IsFeaturedProduct] [bit] NOT NULL,
 CONSTRAINT [PK_CategorySku] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Products]    Script Date: 11/12/2020 1:20:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SKU] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](350) NOT NULL,
	[Description] [nvarchar](1050) NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[SKUPrefix]  AS (substring([SKU],(1),(1))),
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_CategorySku]    Script Date: 11/12/2020 1:20:23 PM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[CategorySku]') AND name = N'IX_CategorySku')
CREATE NONCLUSTERED INDEX [IX_CategorySku] ON [dbo].[CategorySku]
(
	[CategoryId] ASC,
	[SKURange] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_CategorySku_IsFeaturedProduct]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CategorySku] ADD  CONSTRAINT [DF_CategorySku_IsFeaturedProduct]  DEFAULT ((0)) FOR [IsFeaturedProduct]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_Products_Price]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_Price]  DEFAULT ((0.0)) FOR [Price]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CategorySku_Categories]') AND parent_object_id = OBJECT_ID(N'[dbo].[CategorySku]'))
ALTER TABLE [dbo].[CategorySku]  WITH CHECK ADD  CONSTRAINT [FK_CategorySku_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CategorySku_Categories]') AND parent_object_id = OBJECT_ID(N'[dbo].[CategorySku]'))
ALTER TABLE [dbo].[CategorySku] CHECK CONSTRAINT [FK_CategorySku_Categories]
GO
/****** Object:  StoredProcedure [MMTShop].[GetAllCategories]    Script Date: 11/12/2020 1:20:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[MMTShop].[GetAllCategories]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [MMTShop].[GetAllCategories] AS' 
END
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [MMTShop].[GetAllCategories] 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Id, [Name] FROM Categories
END
GO
/****** Object:  StoredProcedure [MMTShop].[GetCategoryProducts]    Script Date: 11/12/2020 1:20:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[MMTShop].[GetCategoryProducts]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [MMTShop].[GetCategoryProducts] AS' 
END
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [MMTShop].[GetCategoryProducts] 
	-- Add the parameters for the stored procedure here
@id int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT p.Id, SKU, p.[Name], [Description], Price, cate.[Name] Category 
	FROM Products p
	INNER JOIN CategorySku sku ON sku.SKURange = p.SKUPrefix
	INNER JOIN Categories cate ON cate.Id = sku.CategoryId
	WHERE cate.Id = @id
END
GO
/****** Object:  StoredProcedure [MMTShop].[GetFeaturedProducts]    Script Date: 11/12/2020 1:20:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[MMTShop].[GetFeaturedProducts]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [MMTShop].[GetFeaturedProducts] AS' 
END
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [MMTShop].[GetFeaturedProducts]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT p.Id, SKU, p.[Name], [Description], Price
	FROM Products p
	INNER JOIN CategorySku sku on sku.SKURange = p.SKUPrefix and sku.IsFeaturedProduct = 1
END
GO
