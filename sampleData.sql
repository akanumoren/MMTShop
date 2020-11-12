USE [MMTShop2]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (1, N'Home')
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (2, N'Garden')
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (3, N'Electronics')
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (4, N'Fitness')
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (5, N'Toys')
GO
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[CategorySku] ON 
GO
INSERT [dbo].[CategorySku] ([Id], [SKURange], [CategoryId], [IsFeaturedProduct]) VALUES (1, N'1', 1, 1)
GO
INSERT [dbo].[CategorySku] ([Id], [SKURange], [CategoryId], [IsFeaturedProduct]) VALUES (2, N'2', 2, 1)
GO
INSERT [dbo].[CategorySku] ([Id], [SKURange], [CategoryId], [IsFeaturedProduct]) VALUES (3, N'3', 3, 1)
GO
INSERT [dbo].[CategorySku] ([Id], [SKURange], [CategoryId], [IsFeaturedProduct]) VALUES (4, N'4', 4, 0)
GO
INSERT [dbo].[CategorySku] ([Id], [SKURange], [CategoryId], [IsFeaturedProduct]) VALUES (5, N'5', 5, 0)
GO
SET IDENTITY_INSERT [dbo].[CategorySku] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 
GO
INSERT [dbo].[Products] ([Id], [SKU], [Name], [Description], [Price]) VALUES (1, N'10001', N'Sofa Bed', N'Luxury sofa bed, made from the finest materials', CAST(2000.99 AS Decimal(18, 2)))
GO
INSERT [dbo].[Products] ([Id], [SKU], [Name], [Description], [Price]) VALUES (2, N'10002', N'Dinning Table', N'Oak dinning table with 6 seats', CAST(5250.50 AS Decimal(18, 2)))
GO
SET IDENTITY_INSERT [dbo].[Products] OFF
GO