IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK_HistoryRow] PRIMARY KEY ([MigrationId])
    );

GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [ConcurrencyStamp] nvarchar(max),
    [Name] nvarchar(256),
    [NormalizedName] nvarchar(256),
    CONSTRAINT [PK_IdentityRole] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [AccessFailedCount] int NOT NULL,
    [ConcurrencyStamp] nvarchar(max),
    [Email] nvarchar(256),
    [EmailConfirmed] bit NOT NULL,
    [LockoutEnabled] bit NOT NULL,
    [LockoutEnd] datetime,
    [NormalizedEmail] nvarchar(256),
    [NormalizedUserName] nvarchar(256),
    [PasswordHash] nvarchar(max),
    [PhoneNumber] nvarchar(max),
    [PhoneNumberConfirmed] bit NOT NULL,
    [SecurityStamp] nvarchar(max),
    [TwoFactorEnabled] bit NOT NULL,
    [UserName] nvarchar(256),
    CONSTRAINT [PK_ApplicationUser] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [ClaimType] nvarchar(max),
    [ClaimValue] nvarchar(max),
    [RoleId] nvarchar(450),
    CONSTRAINT [PK_IdentityRoleClaim<string>] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_IdentityRoleClaim<string>_IdentityRole_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id])
);

GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [ClaimType] nvarchar(max),
    [ClaimValue] nvarchar(max),
    [UserId] nvarchar(450),
    CONSTRAINT [PK_IdentityUserClaim<string>] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_IdentityUserClaim<string>_ApplicationUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id])
);

GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max),
    [UserId] nvarchar(450),
    CONSTRAINT [PK_IdentityUserLogin<string>] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_IdentityUserLogin<string>_ApplicationUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id])
);

GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_IdentityUserRole<string>] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_IdentityUserRole<string>_IdentityRole_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]),
    CONSTRAINT [FK_IdentityUserRole<string>_ApplicationUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id])
);

GO

CREATE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]);

GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);

GO

CREATE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'00000000000000_CreateIdentitySchema', N'7.0.0-rc1-16348');

GO

ALTER TABLE [AspNetRoleClaims] DROP CONSTRAINT [FK_IdentityRoleClaim<string>_IdentityRole_RoleId];

GO

ALTER TABLE [AspNetUserClaims] DROP CONSTRAINT [FK_IdentityUserClaim<string>_ApplicationUser_UserId];

GO

ALTER TABLE [AspNetUserLogins] DROP CONSTRAINT [FK_IdentityUserLogin<string>_ApplicationUser_UserId];

GO

ALTER TABLE [AspNetUserRoles] DROP CONSTRAINT [FK_IdentityUserRole<string>_IdentityRole_RoleId];

GO

ALTER TABLE [AspNetUserRoles] DROP CONSTRAINT [FK_IdentityUserRole<string>_ApplicationUser_UserId];

GO

CREATE TABLE [Empresa] (
    [codigo] nvarchar(450) NOT NULL,
    [LogoTipo] varbinary(max),
    [categoria] nvarchar(max),
    [codEmpresaPri] nvarchar(max),
    [conexao] nvarchar(max),
    [credentials] nvarchar(max),
    [email] nvarchar(max),
    [empresaPrimavera] bit,
    [enableSsl] bit,
    [host] nvarchar(max),
    [localidadeEmpresa] nvarchar(max),
    [morada] nvarchar(max),
    [moradaEmpresa] nvarchar(max),
    [nome] nvarchar(max),
    [nomeEmpresa] nvarchar(max),
    [nuit] nvarchar(max),
    [nuitEmpresa] nvarchar(max),
    [passwordUtilizadorPrimavera] nvarchar(max),
    [port] int,
    [telefoneEmpresa] nvarchar(max),
    [tipoEmpresa] nvarchar(max),
    [useDefaultCredentials] bit,
    [utilizadorPrimavera] nvarchar(max),
    CONSTRAINT [PK_Empresa] PRIMARY KEY ([codigo])
);

GO

CREATE TABLE [Departamento] (
    [Id] int NOT NULL IDENTITY,
    [departamento] nvarchar(max),
    [descricao] nvarchar(max),
    [empresaId] nvarchar(450),
    [responsavelId] nvarchar(450),
    CONSTRAINT [PK_Departamento] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Departamento_Empresa_empresaId] FOREIGN KEY ([empresaId]) REFERENCES [Empresa] ([codigo]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Departamento_ApplicationUser_responsavelId] FOREIGN KEY ([responsavelId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Funcionario] (
    [id] int NOT NULL IDENTITY,
    [categoria] nvarchar(max),
    [classificacao] nvarchar(max),
    [codigo] nvarchar(max),
    [dataAdmissao] datetime2,
    [dataClassificacao] datetime2,
    [dataFimContrato] datetime2,
    [dataNascimento] datetime2,
    [dataReadmissao] datetime2,
    [departamentoId] int NOT NULL,
    [distrito] nvarchar(max),
    [email] nvarchar(max),
    [empresaId] nvarchar(450),
    [estadoCivil] nvarchar(max),
    [habilitacao] nvarchar(max),
    [localidade] nvarchar(max),
    [nacionalidade] nvarchar(max),
    [naturalidade] nvarchar(max),
    [nome] nvarchar(max),
    [profissao] nvarchar(max),
    [sexo] nvarchar(max),
    [telefone] nvarchar(max),
    [telefoneAlternativo] nvarchar(max),
    [telemovel] nvarchar(max),
    [utilizadorId] nvarchar(450),
    CONSTRAINT [PK_Funcionario] PRIMARY KEY ([id]),
    CONSTRAINT [FK_Funcionario_Departamento_departamentoId] FOREIGN KEY ([departamentoId]) REFERENCES [Departamento] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Funcionario_Empresa_empresaId] FOREIGN KEY ([empresaId]) REFERENCES [Empresa] ([codigo]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Funcionario_ApplicationUser_utilizadorId] FOREIGN KEY ([utilizadorId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Ferias] (
    [id] int NOT NULL IDENTITY,
    [dataFim] datetime2 NOT NULL,
    [dataInicio] datetime2 NOT NULL,
    [funcionarioId] int NOT NULL,
    CONSTRAINT [PK_Ferias] PRIMARY KEY ([id]),
    CONSTRAINT [FK_Ferias_Funcionario_funcionarioId] FOREIGN KEY ([funcionarioId]) REFERENCES [Funcionario] ([id]) ON DELETE CASCADE
);

GO

CREATE TABLE [FuncFerias] (
    [id] int NOT NULL IDENTITY,
    [ano] smallint NOT NULL,
    [dataFeria] datetime2 NOT NULL,
    [estadoGozo] bit NOT NULL,
    [funcionarioId] int NOT NULL,
    [originouFalta] bit NOT NULL,
    [originouFaltaSubAlim] bit NOT NULL,
    [tipoMarcacao] int NOT NULL,
    CONSTRAINT [PK_FuncFerias] PRIMARY KEY ([id]),
    CONSTRAINT [FK_FuncFerias_Funcionario_funcionarioId] FOREIGN KEY ([funcionarioId]) REFERENCES [Funcionario] ([id]) ON DELETE CASCADE
);

GO

CREATE TABLE [FuncInfFerias] (
    [id] int NOT NULL IDENTITY,
    [ano] smallint NOT NULL,
    [diasAdicionais] float NOT NULL,
    [diasAnoAnterior] float NOT NULL,
    [diasDireito] float NOT NULL,
    [diasFeriasJaPagas] float NOT NULL,
    [diasJaGozados] float NOT NULL,
    [diasPorGozar] float NOT NULL,
    [diasPorMarcar] float NOT NULL,
    [funcSemFerias] bit NOT NULL,
    [funcionarioId] int NOT NULL,
    [totalDias] float NOT NULL,
    CONSTRAINT [PK_FuncInfFerias] PRIMARY KEY ([id]),
    CONSTRAINT [FK_FuncInfFerias_Funcionario_funcionarioId] FOREIGN KEY ([funcionarioId]) REFERENCES [Funcionario] ([id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Ferias_Itens] (
    [id] int NOT NULL IDENTITY,
    [ano] smallint NOT NULL,
    [dataFeria] datetime2 NOT NULL,
    [estado] nvarchar(max),
    [estadoGozo] bit NOT NULL,
    [feriasId] int,
    [funcionarioId] int NOT NULL,
    [originouFalta] bit NOT NULL,
    [originouFaltaSubAlim] bit NOT NULL,
    [tipo] nvarchar(max),
    [tipoMarcacao] int NOT NULL,
    CONSTRAINT [PK_Ferias_Itens] PRIMARY KEY ([id]),
    CONSTRAINT [FK_Ferias_Itens_Ferias_feriasId] FOREIGN KEY ([feriasId]) REFERENCES [Ferias] ([id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Ferias_Itens_Funcionario_funcionarioId] FOREIGN KEY ([funcionarioId]) REFERENCES [Funcionario] ([id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Historio_Ferias_Item] (
    [id] int NOT NULL IDENTITY,
    [data] datetime2 NOT NULL,
    [estado] nvarchar(max),
    [ferias_item_id] int NOT NULL,
    [utilizadorId] nvarchar(450),
    CONSTRAINT [PK_Historio_Ferias_Item] PRIMARY KEY ([id]),
    CONSTRAINT [FK_Historio_Ferias_Item_Ferias_Itens_ferias_item_id] FOREIGN KEY ([ferias_item_id]) REFERENCES [Ferias_Itens] ([id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Historio_Ferias_Item_ApplicationUser_utilizadorId] FOREIGN KEY ([utilizadorId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'AspNetUserLogins') AND [c].[name] = N'UserId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUserLogins] DROP CONSTRAINT [' + @var0 + ']');
ALTER TABLE [AspNetUserLogins] ALTER COLUMN [UserId] nvarchar(450) NOT NULL;

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'AspNetUserClaims') AND [c].[name] = N'UserId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUserClaims] DROP CONSTRAINT [' + @var1 + ']');
ALTER TABLE [AspNetUserClaims] ALTER COLUMN [UserId] nvarchar(450) NOT NULL;

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'AspNetRoleClaims') AND [c].[name] = N'RoleId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [AspNetRoleClaims] DROP CONSTRAINT [' + @var2 + ']');
ALTER TABLE [AspNetRoleClaims] ALTER COLUMN [RoleId] nvarchar(450) NOT NULL;

GO

ALTER TABLE [AspNetRoleClaims] ADD CONSTRAINT [FK_IdentityRoleClaim<string>_IdentityRole_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [AspNetUserClaims] ADD CONSTRAINT [FK_IdentityUserClaim<string>_ApplicationUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [AspNetUserLogins] ADD CONSTRAINT [FK_IdentityUserLogin<string>_ApplicationUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [AspNetUserRoles] ADD CONSTRAINT [FK_IdentityUserRole<string>_IdentityRole_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [AspNetUserRoles] ADD CONSTRAINT [FK_IdentityUserRole<string>_ApplicationUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20151203100523_first_commit', N'7.0.0-rc1-16348');

GO

ALTER TABLE [AspNetRoleClaims] DROP CONSTRAINT [FK_IdentityRoleClaim<string>_IdentityRole_RoleId];

GO

ALTER TABLE [AspNetUserClaims] DROP CONSTRAINT [FK_IdentityUserClaim<string>_ApplicationUser_UserId];

GO

ALTER TABLE [AspNetUserLogins] DROP CONSTRAINT [FK_IdentityUserLogin<string>_ApplicationUser_UserId];

GO

ALTER TABLE [AspNetUserRoles] DROP CONSTRAINT [FK_IdentityUserRole<string>_IdentityRole_RoleId];

GO

ALTER TABLE [AspNetUserRoles] DROP CONSTRAINT [FK_IdentityUserRole<string>_ApplicationUser_UserId];

GO

ALTER TABLE [Ferias] DROP CONSTRAINT [FK_Ferias_Funcionario_funcionarioId];

GO

ALTER TABLE [Ferias_Itens] DROP CONSTRAINT [FK_Ferias_Itens_Funcionario_funcionarioId];

GO

ALTER TABLE [FuncFerias] DROP CONSTRAINT [FK_FuncFerias_Funcionario_funcionarioId];

GO

ALTER TABLE [FuncInfFerias] DROP CONSTRAINT [FK_FuncInfFerias_Funcionario_funcionarioId];

GO

ALTER TABLE [Funcionario] DROP CONSTRAINT [FK_Funcionario_Departamento_departamentoId];

GO

ALTER TABLE [Historio_Ferias_Item] DROP CONSTRAINT [FK_Historio_Ferias_Item_Ferias_Itens_ferias_item_id];

GO

ALTER TABLE [AspNetRoleClaims] ADD CONSTRAINT [FK_IdentityRoleClaim<string>_IdentityRole_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [AspNetUserClaims] ADD CONSTRAINT [FK_IdentityUserClaim<string>_ApplicationUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [AspNetUserLogins] ADD CONSTRAINT [FK_IdentityUserLogin<string>_ApplicationUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [AspNetUserRoles] ADD CONSTRAINT [FK_IdentityUserRole<string>_IdentityRole_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [AspNetUserRoles] ADD CONSTRAINT [FK_IdentityUserRole<string>_ApplicationUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [Ferias] ADD CONSTRAINT [FK_Ferias_Funcionario_funcionarioId] FOREIGN KEY ([funcionarioId]) REFERENCES [Funcionario] ([id]) ON DELETE CASCADE;

GO

ALTER TABLE [Ferias_Itens] ADD CONSTRAINT [FK_Ferias_Itens_Funcionario_funcionarioId] FOREIGN KEY ([funcionarioId]) REFERENCES [Funcionario] ([id]) ON DELETE CASCADE;

GO

ALTER TABLE [FuncFerias] ADD CONSTRAINT [FK_FuncFerias_Funcionario_funcionarioId] FOREIGN KEY ([funcionarioId]) REFERENCES [Funcionario] ([id]) ON DELETE CASCADE;

GO

ALTER TABLE [FuncInfFerias] ADD CONSTRAINT [FK_FuncInfFerias_Funcionario_funcionarioId] FOREIGN KEY ([funcionarioId]) REFERENCES [Funcionario] ([id]) ON DELETE CASCADE;

GO

ALTER TABLE [Funcionario] ADD CONSTRAINT [FK_Funcionario_Departamento_departamentoId] FOREIGN KEY ([departamentoId]) REFERENCES [Departamento] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [Historio_Ferias_Item] ADD CONSTRAINT [FK_Historio_Ferias_Item_Ferias_Itens_ferias_item_id] FOREIGN KEY ([ferias_item_id]) REFERENCES [Ferias_Itens] ([id]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20151204125446_add_display_name', N'7.0.0-rc1-16348');

GO

ALTER TABLE [AspNetRoleClaims] DROP CONSTRAINT [FK_IdentityRoleClaim<string>_IdentityRole_RoleId];

GO

ALTER TABLE [AspNetUserClaims] DROP CONSTRAINT [FK_IdentityUserClaim<string>_ApplicationUser_UserId];

GO

ALTER TABLE [AspNetUserLogins] DROP CONSTRAINT [FK_IdentityUserLogin<string>_ApplicationUser_UserId];

GO

ALTER TABLE [AspNetUserRoles] DROP CONSTRAINT [FK_IdentityUserRole<string>_IdentityRole_RoleId];

GO

ALTER TABLE [AspNetUserRoles] DROP CONSTRAINT [FK_IdentityUserRole<string>_ApplicationUser_UserId];

GO

ALTER TABLE [Ferias] DROP CONSTRAINT [FK_Ferias_Funcionario_funcionarioId];

GO

ALTER TABLE [Ferias_Itens] DROP CONSTRAINT [FK_Ferias_Itens_Funcionario_funcionarioId];

GO

ALTER TABLE [FuncFerias] DROP CONSTRAINT [FK_FuncFerias_Funcionario_funcionarioId];

GO

ALTER TABLE [FuncInfFerias] DROP CONSTRAINT [FK_FuncInfFerias_Funcionario_funcionarioId];

GO

ALTER TABLE [Funcionario] DROP CONSTRAINT [FK_Funcionario_Departamento_departamentoId];

GO

ALTER TABLE [Historio_Ferias_Item] DROP CONSTRAINT [FK_Historio_Ferias_Item_Ferias_Itens_ferias_item_id];

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'Departamento') AND [c].[name] = N'descricao');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Departamento] DROP CONSTRAINT [' + @var3 + ']');
ALTER TABLE [Departamento] ALTER COLUMN [descricao] nvarchar(max) NOT NULL;

GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'Departamento') AND [c].[name] = N'departamento');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Departamento] DROP CONSTRAINT [' + @var4 + ']');
ALTER TABLE [Departamento] ALTER COLUMN [departamento] nvarchar(max) NOT NULL;

GO

ALTER TABLE [Departamento] ADD [activo] bit NOT NULL DEFAULT 0;

GO

ALTER TABLE [AspNetRoleClaims] ADD CONSTRAINT [FK_IdentityRoleClaim<string>_IdentityRole_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [AspNetUserClaims] ADD CONSTRAINT [FK_IdentityUserClaim<string>_ApplicationUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [AspNetUserLogins] ADD CONSTRAINT [FK_IdentityUserLogin<string>_ApplicationUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [AspNetUserRoles] ADD CONSTRAINT [FK_IdentityUserRole<string>_IdentityRole_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [AspNetUserRoles] ADD CONSTRAINT [FK_IdentityUserRole<string>_ApplicationUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [Ferias] ADD CONSTRAINT [FK_Ferias_Funcionario_funcionarioId] FOREIGN KEY ([funcionarioId]) REFERENCES [Funcionario] ([id]) ON DELETE CASCADE;

GO

ALTER TABLE [Ferias_Itens] ADD CONSTRAINT [FK_Ferias_Itens_Funcionario_funcionarioId] FOREIGN KEY ([funcionarioId]) REFERENCES [Funcionario] ([id]) ON DELETE CASCADE;

GO

ALTER TABLE [FuncFerias] ADD CONSTRAINT [FK_FuncFerias_Funcionario_funcionarioId] FOREIGN KEY ([funcionarioId]) REFERENCES [Funcionario] ([id]) ON DELETE CASCADE;

GO

ALTER TABLE [FuncInfFerias] ADD CONSTRAINT [FK_FuncInfFerias_Funcionario_funcionarioId] FOREIGN KEY ([funcionarioId]) REFERENCES [Funcionario] ([id]) ON DELETE CASCADE;

GO

ALTER TABLE [Funcionario] ADD CONSTRAINT [FK_Funcionario_Departamento_departamentoId] FOREIGN KEY ([departamentoId]) REFERENCES [Departamento] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [Historio_Ferias_Item] ADD CONSTRAINT [FK_Historio_Ferias_Item_Ferias_Itens_ferias_item_id] FOREIGN KEY ([ferias_item_id]) REFERENCES [Ferias_Itens] ([id]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20151204130150_add_estado_activo_departamento_name', N'7.0.0-rc1-16348');

GO

ALTER TABLE [AspNetRoleClaims] DROP CONSTRAINT [FK_IdentityRoleClaim<string>_IdentityRole_RoleId];

GO

ALTER TABLE [AspNetUserClaims] DROP CONSTRAINT [FK_IdentityUserClaim<string>_ApplicationUser_UserId];

GO

ALTER TABLE [AspNetUserLogins] DROP CONSTRAINT [FK_IdentityUserLogin<string>_ApplicationUser_UserId];

GO

ALTER TABLE [AspNetUserRoles] DROP CONSTRAINT [FK_IdentityUserRole<string>_IdentityRole_RoleId];

GO

ALTER TABLE [AspNetUserRoles] DROP CONSTRAINT [FK_IdentityUserRole<string>_ApplicationUser_UserId];

GO

ALTER TABLE [Ferias] DROP CONSTRAINT [FK_Ferias_Funcionario_funcionarioId];

GO

ALTER TABLE [Ferias_Itens] DROP CONSTRAINT [FK_Ferias_Itens_Funcionario_funcionarioId];

GO

ALTER TABLE [FuncFerias] DROP CONSTRAINT [FK_FuncFerias_Funcionario_funcionarioId];

GO

ALTER TABLE [FuncInfFerias] DROP CONSTRAINT [FK_FuncInfFerias_Funcionario_funcionarioId];

GO

ALTER TABLE [Funcionario] DROP CONSTRAINT [FK_Funcionario_Departamento_departamentoId];

GO

ALTER TABLE [Historio_Ferias_Item] DROP CONSTRAINT [FK_Historio_Ferias_Item_Ferias_Itens_ferias_item_id];

GO

ALTER TABLE [AspNetRoleClaims] ADD CONSTRAINT [FK_IdentityRoleClaim<string>_IdentityRole_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [AspNetUserClaims] ADD CONSTRAINT [FK_IdentityUserClaim<string>_ApplicationUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [AspNetUserLogins] ADD CONSTRAINT [FK_IdentityUserLogin<string>_ApplicationUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [AspNetUserRoles] ADD CONSTRAINT [FK_IdentityUserRole<string>_IdentityRole_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [AspNetUserRoles] ADD CONSTRAINT [FK_IdentityUserRole<string>_ApplicationUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [Ferias] ADD CONSTRAINT [FK_Ferias_Funcionario_funcionarioId] FOREIGN KEY ([funcionarioId]) REFERENCES [Funcionario] ([id]) ON DELETE CASCADE;

GO

ALTER TABLE [Ferias_Itens] ADD CONSTRAINT [FK_Ferias_Itens_Funcionario_funcionarioId] FOREIGN KEY ([funcionarioId]) REFERENCES [Funcionario] ([id]) ON DELETE CASCADE;

GO

ALTER TABLE [FuncFerias] ADD CONSTRAINT [FK_FuncFerias_Funcionario_funcionarioId] FOREIGN KEY ([funcionarioId]) REFERENCES [Funcionario] ([id]) ON DELETE CASCADE;

GO

ALTER TABLE [FuncInfFerias] ADD CONSTRAINT [FK_FuncInfFerias_Funcionario_funcionarioId] FOREIGN KEY ([funcionarioId]) REFERENCES [Funcionario] ([id]) ON DELETE CASCADE;

GO

ALTER TABLE [Funcionario] ADD CONSTRAINT [FK_Funcionario_Departamento_departamentoId] FOREIGN KEY ([departamentoId]) REFERENCES [Departamento] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [Historio_Ferias_Item] ADD CONSTRAINT [FK_Historio_Ferias_Item_Ferias_Itens_ferias_item_id] FOREIGN KEY ([ferias_item_id]) REFERENCES [Ferias_Itens] ([id]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20151207233044_update_data', N'7.0.0-rc1-16348');

GO

ALTER TABLE [AspNetRoleClaims] DROP CONSTRAINT [FK_IdentityRoleClaim<string>_IdentityRole_RoleId];

GO

ALTER TABLE [AspNetUserClaims] DROP CONSTRAINT [FK_IdentityUserClaim<string>_ApplicationUser_UserId];

GO

ALTER TABLE [AspNetUserLogins] DROP CONSTRAINT [FK_IdentityUserLogin<string>_ApplicationUser_UserId];

GO

ALTER TABLE [AspNetUserRoles] DROP CONSTRAINT [FK_IdentityUserRole<string>_IdentityRole_RoleId];

GO

ALTER TABLE [AspNetUserRoles] DROP CONSTRAINT [FK_IdentityUserRole<string>_ApplicationUser_UserId];

GO

ALTER TABLE [Ferias] DROP CONSTRAINT [FK_Ferias_Funcionario_funcionarioId];

GO

ALTER TABLE [Ferias_Itens] DROP CONSTRAINT [FK_Ferias_Itens_Funcionario_funcionarioId];

GO

ALTER TABLE [FuncFerias] DROP CONSTRAINT [FK_FuncFerias_Funcionario_funcionarioId];

GO

ALTER TABLE [FuncInfFerias] DROP CONSTRAINT [FK_FuncInfFerias_Funcionario_funcionarioId];

GO

ALTER TABLE [Funcionario] DROP CONSTRAINT [FK_Funcionario_Departamento_departamentoId];

GO

ALTER TABLE [Historio_Ferias_Item] DROP CONSTRAINT [FK_Historio_Ferias_Item_Ferias_Itens_ferias_item_id];

GO

ALTER TABLE [AspNetRoleClaims] ADD CONSTRAINT [FK_IdentityRoleClaim<string>_IdentityRole_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [AspNetUserClaims] ADD CONSTRAINT [FK_IdentityUserClaim<string>_ApplicationUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [AspNetUserLogins] ADD CONSTRAINT [FK_IdentityUserLogin<string>_ApplicationUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [AspNetUserRoles] ADD CONSTRAINT [FK_IdentityUserRole<string>_IdentityRole_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [AspNetUserRoles] ADD CONSTRAINT [FK_IdentityUserRole<string>_ApplicationUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [Ferias] ADD CONSTRAINT [FK_Ferias_Funcionario_funcionarioId] FOREIGN KEY ([funcionarioId]) REFERENCES [Funcionario] ([id]) ON DELETE CASCADE;

GO

ALTER TABLE [Ferias_Itens] ADD CONSTRAINT [FK_Ferias_Itens_Funcionario_funcionarioId] FOREIGN KEY ([funcionarioId]) REFERENCES [Funcionario] ([id]) ON DELETE CASCADE;

GO

ALTER TABLE [FuncFerias] ADD CONSTRAINT [FK_FuncFerias_Funcionario_funcionarioId] FOREIGN KEY ([funcionarioId]) REFERENCES [Funcionario] ([id]) ON DELETE CASCADE;

GO

ALTER TABLE [FuncInfFerias] ADD CONSTRAINT [FK_FuncInfFerias_Funcionario_funcionarioId] FOREIGN KEY ([funcionarioId]) REFERENCES [Funcionario] ([id]) ON DELETE CASCADE;

GO

ALTER TABLE [Funcionario] ADD CONSTRAINT [FK_Funcionario_Departamento_departamentoId] FOREIGN KEY ([departamentoId]) REFERENCES [Departamento] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [Historio_Ferias_Item] ADD CONSTRAINT [FK_Historio_Ferias_Item_Ferias_Itens_ferias_item_id] FOREIGN KEY ([ferias_item_id]) REFERENCES [Ferias_Itens] ([id]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20151208150745_update_query', N'7.0.0-rc1-16348');

GO


