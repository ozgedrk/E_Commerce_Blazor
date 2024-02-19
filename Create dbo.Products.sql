USE [E_CommerceNet7]
GO

/****** Object: Table [dbo].[Products] Script Date: 14.02.2024 15:14:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Products] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [Name]              NVARCHAR (MAX) NOT NULL,
    [Description]       NVARCHAR (MAX) NOT NULL,
    [ShopFavorites]     BIT            NOT NULL,
    [CustomerFavorites] BIT            NOT NULL,
    [Author]            NVARCHAR (MAX) NOT NULL,
    [ImageUrl]          NVARCHAR (MAX) NOT NULL,
    [CategoryId]        INT            NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_Products_CategoryId]
    ON [dbo].[Products]([CategoryId] ASC);


GO
ALTER TABLE [dbo].[Products]
    ADD CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([Id] ASC);


GO
ALTER TABLE [dbo].[Products]
    ADD CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([Id]) ON DELETE CASCADE;


