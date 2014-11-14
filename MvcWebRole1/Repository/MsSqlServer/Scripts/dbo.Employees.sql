CREATE TABLE [dbo].[Employees] (
    [ID]       INT       IDENTITY(1,1)    NOT NULL,
    [Name]     NVARCHAR (50) NOT NULL,
    [Title]    NVARCHAR (64) NOT NULL,
    [Location] NVARCHAR (64) NULL,
    [Email]    NVARCHAR (64) NULL,
    [Phone]    NVARCHAR (20) NULL,
    [Role]     INT           NOT NULL,
    CONSTRAINT [PrimaryKey_a1eec6eb-be58-4edf-b22a-3afa35b113b2] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Employees_0]
    ON [dbo].[Employees]([Name] ASC, [Role] ASC);

