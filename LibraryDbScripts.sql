CREATE TABLE Users (
    Id int IDENTITY(1,1) PRIMARY KEY,
    FirstName varchar(255),
	LastName varchar(255) NOT NULL,
	UserName varchar(255) NOT NULL,
    Password varchar(255) NOT NULL,
	CreateDate Datetime not null,
	LastUpdateDate Datetime not null,
);

Create table Categories (
	Id int IDENTITY(1,1) PRIMARY KEY,
	CategoryName varchar(255) NOT NULL,
	CategoryDescription varchar(max) NOT NULL,
	CreateDate Datetime not null,
	LastUpdateDate Datetime not null
);

Create table BookTypes(
	Id int IDENTITY(1,1) PRIMARY KEY,
	BookTypeName varchar(255) NOT NULL
);

Create table UserLoginTokens(
	Id int IDENTITY(1,1) PRIMARY KEY,
	UserId int NOT NULL,
	Token varchar(255) NOT NULL,
	CreateDate DateTime NOT NULL,
	ExpiryDate DateTime NOT NULL
);

Create table Books (
	Id int IDENTITY(1,1) PRIMARY KEY,
	BookName varchar(255) NOT NULL,
	BookType int not null,
	Category int not null,
	Subcategory int,
	WrittenBy varchar(255) NOT NULL,
	DateOfIssue Datetime not null,
	BookStatus int not null,
	CreateDate Datetime not null,
	LastUpdateDate Datetime not null,
	LastUpdateBy int
	FOREIGN KEY (LastUpdateBy) REFERENCES Users(Id),
	FOREIGN KEY (BookType) REFERENCES BookTypes(Id),
	FOREIGN KEY (Category) REFERENCES Categories(Id)

);



INSERT INTO BookTypes (BookTypeName) VALUES ('Roman');
INSERT INTO BookTypes (BookTypeName) VALUES ('Kurgu');
INSERT INTO BookTypes (BookTypeName) VALUES ('Bilim Kurgu');
INSERT INTO BookTypes (BookTypeName) VALUES ('Mistik');
INSERT INTO BookTypes (BookTypeName) VALUES ('Biografi');

-- Categories Tablosu için örnek veri ekleme
INSERT INTO Categories (CategoryName, CategoryDescription, CreateDate, LastUpdateDate) 
VALUES ('Klasik', 'Klasik kitaplar kategorisi', GETDATE(), GETDATE());
INSERT INTO Categories (CategoryName, CategoryDescription, CreateDate, LastUpdateDate) 
VALUES ('Bilim Kurgu', 'Bilim kurgu kitapları kategorisi', GETDATE(), GETDATE());
INSERT INTO Categories (CategoryName, CategoryDescription, CreateDate, LastUpdateDate) 
VALUES ('Tarih', 'Tarih kitapları kategorisi', GETDATE(), GETDATE());
INSERT INTO Categories (CategoryName, CategoryDescription, CreateDate, LastUpdateDate) 
VALUES ('Biyografi', 'Biyografi kitapları kategorisi', GETDATE(), GETDATE());
INSERT INTO Categories (CategoryName, CategoryDescription, CreateDate, LastUpdateDate) 
VALUES ('Felsefe', 'Felsefe kitapları kategorisi', GETDATE(), GETDATE());

-- Books Tablosu için örnek veri ekleme
