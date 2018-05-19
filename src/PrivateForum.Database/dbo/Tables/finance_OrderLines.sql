CREATE TABLE [dbo].[finance_OrderLines]
(
	[Id] INT NOT NULL IDENTITY (1, 1),
	[Product]       NVARCHAR(MAX)     NOT NULL,
	[Amount]        DECIMAL (18,4)    NOT NULL,
	[ProgramId]     INT               NULL,
	[OrderId]       INT               NOT NULL,
	[PromotionId]   INT               NULL,
	[OrderLineType] INT               NOT NULL DEFAULT 1,
	[PaymentChunk]  INT               NULL,
	[Created]       DATETIME          NULL,
	[Modified]      DATETIME          NULL,
	[CreatedBy]     NVARCHAR(50),
	[ModifiedBy]    NVARCHAR(50),
    CONSTRAINT [PK_dbo.finance_OrderLines] PRIMARY KEY CLUSTERED ([Id] ASC)
)
