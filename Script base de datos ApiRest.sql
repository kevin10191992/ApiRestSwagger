USE [master]
GO
CREATE DATABASE [kprTest]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'kprTest', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\kprTest.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'kprTest_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\kprTest_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [kprTest] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [kprTest].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [kprTest] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [kprTest] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [kprTest] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [kprTest] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [kprTest] SET ARITHABORT OFF 
GO
ALTER DATABASE [kprTest] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [kprTest] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [kprTest] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [kprTest] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [kprTest] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [kprTest] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [kprTest] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [kprTest] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [kprTest] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [kprTest] SET  DISABLE_BROKER 
GO
ALTER DATABASE [kprTest] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [kprTest] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [kprTest] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [kprTest] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [kprTest] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [kprTest] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [kprTest] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [kprTest] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [kprTest] SET  MULTI_USER 
GO
ALTER DATABASE [kprTest] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [kprTest] SET DB_CHAINING OFF 
GO
ALTER DATABASE [kprTest] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [kprTest] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [kprTest] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [kprTest] SET QUERY_STORE = OFF
GO
USE [kprTest]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persona](
	[PersonaId] [int] IDENTITY(1,1) NOT NULL,
	[Nombres] [varchar](50) NOT NULL,
	[Apellidos] [varchar](50) NOT NULL,
	[Cedula] [varchar](20) NOT NULL,
	[Genero] [varchar](1) NOT NULL,
	[Edad] [int] NOT NULL,
 CONSTRAINT [PK_Persona] PRIMARY KEY CLUSTERED 
(
	[PersonaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CREATEMODEL]  
(  
     @TableName SYSNAME ,  
     @CLASSNAME VARCHAR(500)   
)  
AS  
BEGIN  
    DECLARE @Result VARCHAR(MAX)  
    SET @Result = @CLASSNAME + @TableName + '  
{'  
SELECT @Result = @Result + '  
    public ' + ColumnType + NullableSign + ' ' + ColumnName + ' { get; set; }'  
FROM  
(  
    SELECT   
        REPLACE(col.NAME, ' ', '_') ColumnName,  
        column_id ColumnId,  
        CASE typ.NAME   
            WHEN 'bigint' THEN 'long'  
            WHEN 'binary' THEN 'byte[]'  
            WHEN 'bit' THEN 'bool'  
            WHEN 'char' THEN 'string'  
            WHEN 'date' THEN 'DateTime'  
            WHEN 'datetime' THEN 'DateTime'  
            WHEN 'datetime2' then 'DateTime'  
            WHEN 'datetimeoffset' THEN 'DateTimeOffset'  
            WHEN 'decimal' THEN 'decimal'  
            WHEN 'float' THEN 'float'  
            WHEN 'image' THEN 'byte[]'  
            WHEN 'int' THEN 'int'  
            WHEN 'money' THEN 'decimal'  
            WHEN 'nchar' THEN 'char'  
            WHEN 'ntext' THEN 'string'  
            WHEN 'numeric' THEN 'decimal'  
            WHEN 'nvarchar' THEN 'string'  
            WHEN 'real' THEN 'double'  
            WHEN 'smalldatetime' THEN 'DateTime'  
            WHEN 'smallint' THEN 'short'  
            WHEN 'smallmoney' THEN 'decimal'  
            WHEN 'text' THEN 'string'  
            WHEN 'time' THEN 'TimeSpan'  
            WHEN 'timestamp' THEN 'DateTime'  
            WHEN 'tinyint' THEN 'byte'  
            WHEN 'uniqueidentifier' THEN 'Guid'  
            WHEN 'varbinary' THEN 'byte[]'  
            WHEN 'varchar' THEN 'string'  
            ELSE 'UNKNOWN_' + typ.NAME  
        END ColumnType,  
        CASE   
            WHEN col.is_nullable = 1 and typ.NAME in ('bigint', 'bit', 'date', 'datetime', 'datetime2', 'datetimeoffset', 'decimal', 'float', 'int', 'money', 'numeric', 'real', 'smalldatetime', 'smallint', 'smallmoney', 'time', 'tinyint', 'uniqueidentifier')   
            THEN '?'   
            ELSE ''   
        END NullableSign  
    FROM SYS.COLUMNS col join sys.types typ on col.system_type_id = typ.system_type_id AND col.user_type_id = typ.user_type_id  
    where object_id = object_id(@TableName)  
) t  
ORDER BY ColumnId  
SET @Result = @Result  + '  
}'  
print @Result  
END
GO
USE [master]
GO
ALTER DATABASE [kprTest] SET  READ_WRITE 
GO
