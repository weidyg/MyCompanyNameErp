# 快速开始 
### 1、修改迁移配置

```
{
  "ConnectionStrings": {
    "Default": "Server=127.0.0.1;Port=3306;Database=_ErpDefaultTest;charset=utf8;user=root;password=123456;",
    "Tenant": "Server=127.0.0.1;Port=3306;Database=_ErpMainTest;charset=utf8;user=root;password=123456;",
    "Identity": "Server=127.0.0.1;Port=3306;Database=_ErpMainTest;charset=utf8;user=root;password=123456;"
  },
  "Redis": {
    "Configuration": "127.0.0.1"
  }
}
```
### 2、修改站点配置
```
 "AbpAliyunSms": {
    "Endpoint": "dysmsapi.aliyuncs.com",
    "AccessKeyId": "",
    "AccessKeySecret": ""
  },
  "Minio": {
    "EndPoint": "127.0.0.1:9000",
    "AccessKey": "minioadmin",
    "SecretKey": "minioadmin"
  },
  "ConnectionStrings": {
    "Default": "Server=127.0.0.1;Port=3306;Database=_ErpDefaultTest;charset=utf8;user=root;password=123456;",
    "Tenant": "Server=127.0.0.1;Port=3306;Database=_ErpMainTest;charset=utf8;user=root;password=123456;",
    "Identity": "Server=127.0.0.1;Port=3306;Database=_ErpMainTest;charset=utf8;user=root;password=123456;"
  },
  "Redis": {
    "Configuration": "127.0.0.1"
  }
```
### 3、数据库迁移
>运行`MyCompanyName.Erp.DbMigrator`项目
