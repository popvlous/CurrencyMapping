# CurrencyMapping

代碼中文化功能

## 功能簡述
- 呼叫 coindesk API，解析其下行內容與資料轉換，並實作新的 API。
coindesk API：https://api.coindesk.com/v1/bpi/currentprice.json
- 建立一張幣別與其對應中文名稱的資料表（需附建立SQL語法），並提供查詢/新增/修改/刪除功能 API。
- 查詢幣別請依照幣別代碼排序。

## 實作內容
- 幣別 DB 維護功能。
    - 查詢DB所有幣別代碼與對應中文 
        - [GET] /api/Currencies

    - 查詢DB單一幣別代碼與對應中文 
        - [GET] /api/Currencies/{code}
      
    - 新增DB幣別代碼與對應中文 
        - [POST] /api/Currencies
        
    - 編輯DB幣別代碼與對應中文 
        - [PUT] /api/Currencies/{code}
        - 
    - 刪除DB幣別代碼與對應中文
        - [DELETE] /api/Currencies/{code}

- 呼叫 coindesk 的 API。
    - [GET] /api/CoindeskApi
    - 
- 呼叫 coindesk 的 API，並進行資料轉換，組成新 API。此新 API 提供：
    - 更新時間（時間格式範例：1990/01/01 00:00:00）。
    - 幣別相關資訊（幣別，幣別中文名稱，以及匯率）。
    - [GET] /api/CoindeskApi/CreateCurrenyMappingApi
    - 
- 所有功能均須包含單元測試。
    - CoindeskApiTest : CoindeskApiController的測試
    - CurrenciesTest  : CurrenciesController的測試
    
- 將專案上傳至 GitHub 並設為公開分享，回傳repo鏈結。
    - https://github.com/popvlous/CurrencyMapping.git

- 嘗試錄製demo，上傳至Youtube影片(不要用Shorts)，設為非公開分享回傳

## 使用說明

- 資料庫語法

-- 建立資料庫
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'currency')
BEGIN
    CREATE DATABASE currency;
END
GO

-- 使用新建立的資料庫
USE currency;
GO

-- 建立Currency資料表
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Currency')
BEGIN
    CREATE TABLE Currency (
        code NVARCHAR(50) PRIMARY KEY,
        cname NVARCHAR(50) NOT NULL
    );
END
GO

-- 插入Currency資料
INSERT INTO Currency (code, cname)
VALUES 
    (N'USD', N'美金')


- DB連結

  "ConnectionStrings": {
    "CurrencyMappingContext": "Data Source=(localdb)\\ProjectModels;Initial Catalog=currency;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"
  }

## 開發工具

- .NET Core 8
- Visual Studio 2022
- MS SQL(LocalDB)
- swagger-ui

- 
- ## 條件
- 印出所有 API 被呼叫以及呼叫外部 API 的 request and response body log
    -使用LoggerLocalFile紀錄
- Error handling 處理 API response
    -使用middleware處理Error Error handlin
- swagger-ui
    -https://localhost:44392/swagger/index.html
- 多語系設計
- design pattern 實作
- 能夠運行在 Docker
- 加解密技術應用 (AES/RSA…etc.)