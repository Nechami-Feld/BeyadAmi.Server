# Database Context

מערכת:
מערכת ניהול ארגון השאלת מכשירים רפואיים ללא תשלום.

Database:
SQL Server

ORM:
Entity Framework Core 9

כללי Database:
- שמות טבלאות באנגלית וברבים
- Primary Key בשם EntityId
- Foreign Keys בשם EntityId
- אין שכפול מידע
- שימוש ב-Lookup Tables כאשר יש ערכים קבועים
- שימוש ב-Normalization עד 3NF


## Tables


## Branches

מטרה:
ניהול סניפי הארגון.

Fields:

BranchId
- INT
- PK

BranchName
- NVARCHAR(100)
- Required

City
- NVARCHAR(100)

Street
- NVARCHAR(100)

Apartment
- NVARCHAR(20)

ManagerLastName
- NVARCHAR(100)

Phone
- VARCHAR(20)

Email
- VARCHAR(150)

Notes
- NVARCHAR(MAX)

IsActive
- BIT


Relationships:

Branch 1 ---> Many Devices

Branch 1 ---> Many BranchRequests

Branch 1 ---> Many SurveySendings



## DeviceCategories

מטרה:
קטגוריות מכשירים.

Examples:
- אדים קרים
- אדים חמים
- אייפופר


Fields:

CategoryId
CategoryName
Description


Relationship:

DeviceCategory 1 ---> Many DeviceTypes



## DeviceTypes

מטרה:
סוג ודגם מכשיר.

Fields:

DeviceTypeId

CategoryId FK

DeviceName

Company

Model

BasicInfo

Rules


Relationship:

DeviceType 1 ---> Many Devices



## Devices

מטרה:
מכשיר פיזי בארגון.


Fields:

DeviceId

DeviceTypeId FK

BranchId FK

DeviceNumber

Company

Notes

CreatedDate


Rules:

אין לשמור IsLoaned.
מצב השאלה מחושב לפי Loans פתוחות.


Relationships:

Device 1 ---> Many Loans



## Loans

מטרה:
ניהול השאלות.


Fields:

LoanId

DeviceId FK

FirstName

LastName

Address

Phone

DepositTypeId FK

DepositAmount

LoanDate

ReturnDate

Notes


Loan Active:
ReturnDate IS NULL



## DepositTypes

Values:

1 - Money
2 - Check
3 - None



## Stores

חנויות לרכישת ציוד.


Fields:

StoreId

StoreName

Address

Phone

Notes



## Products

מוצרים.


Fields:

ProductId

ProductName

Model

Company



## StoreProducts

קישור מוצר לחנות.


Fields:

StoreProductId

StoreId FK

ProductId FK

Price



## Purchases

היסטוריית רכישות.


Fields:

PurchaseId

StoreId FK

ProductId FK

Quantity

PricePerUnit

TotalPrice

BuyerName

PurchaseDate

ReceiptFile

Notes



## RequiredProducts

מוצרים שכדאי לרכוש.


Fields:

RequiredProductId

ProductName

Model

Company

Quantity

Notes



## BranchRequests

בקשות סניפים.


Fields:

RequestId

BranchId FK

RequestDate

Description

IsCompleted

CompletedDate

Notes



## DeviceTemplates

תבניות להדפסת תוויות.


Fields:

TemplateId

DeviceTypeId FK

TemplateName

FilePath

CreatedDate



## SurveyQuestions

שאלות.


Fields:

QuestionId

QuestionText

OrderNumber

IsActive



## SurveySendings

שליחת שאלונים.


Fields:

SurveySendId

BranchId FK

SendDate

Token

IsAnswered



## SurveyAnswers

תשובות.


Fields:

AnswerId

SurveySendId FK

QuestionId FK

AnswerText

ManagerNotes

AnswerDate