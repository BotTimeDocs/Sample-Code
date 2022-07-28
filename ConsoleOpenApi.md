# 云扩控制台API文档

更新日期：2022-07-28

## 文档概述

本文档描述了控制台产品对外提供的API接口的详细描述，包括接口用途、请求格式、返回格式和请求实例等。

## 面向对象

本文档主要面向以下对象：
● 二次开发工程师

## 调用方式

### 构造请求

#### 服务端点

本文档主要涉及ApiGateway和SSO两个服务端点。ApiGateway为后台接口提供统一的调用入口，SSO负责提供统一的认证鉴权服务。对于需要认证的接口，均需要在Authorization请求头中带上SSO签发的token信息，格式为Authorization: Bearer &lt;token&gt;。
说明：
Bearer和&lt;token&gt;之间需留一个空格。
● SaaS服务端点：
  > SSO: auth.encoo.com
  ApiGateway:api.encoo.com

● 私有化服务端点：
默认情况下，非https部署，SSO端口为81，ApiGateway端口为8080。 https部署情况下，为对应域名
  >SSO:控制台IP:81或SSO域名
  ApiGateway:控制台IP:8080或ApiGateway域名

#### 请求URI

请求URI由如下部分组成。
{URI-scheme}:</span>//{Endpoint}/{resource-path}?{query-string}
尽管请求URI包含在请求消息头中，但大多数语言或框架都要求您从请求消息中单独传递它，所以在此单独强调。

|参数| 描述|
|-|-|
|URI-scheme| 表示用于传输请求的协议，当前API接口采用HTTPS协议或HTTP协议。
|Endpoint| 指定承载API服务的服务器域名或IP地址加端口号，即调用API服务的接入地址。
|resource-path| 资源路径，也即API的请求路径。从具体API的URI模块获取。
|query-string| 公共请求参数，请求参数前面需要携带“?”，形式为“参数名=参数取值”，例如“AccessKey=d0742694e5784074af7b2c5ecff21455”，参数之间由“&”连接。

#### 请求方法

|方法|说明|
|-|-|
|GET| 请求服务器返回指定资源。
|PUT| 请求服务器更新指定资源。
|POST| 请求服务器新增资源或执行特殊操作。
|DELETE| 请求服务器删除指定资源，如删除对象等。
|HEAD| 请求服务器资源头部。
|PATCH| 请求服务器更新资源的部分内容。

#### 请求消息头

**公共请求消息头是所有API请求都必需的参数。为减少内容重复，公共请求将不在各API详情中列出。**
|名称|描述|示例|
|-|-|-|
|Content-type| 指定请求消息体中的MIME类型| application/json。**本系中若未特殊说明通常为application/json**,上传文件时通常为application/octet-stream|
|~~Content-Length~~| 指定请求消息体的长度| 3456。**通常可省略**，因为HttpClient，PostMan等会自动添加
|Authorization| 指定用户的认证令牌token| Bearer eyJhbGciOiJSUzI1Ni…(后略)即Bearer 拼接通过端口获取到的3.2.2获取到的令牌|
|CompanyId| 需要调用接口获取的信息所在公司Id| 40143948-f359-4b9f-949f-ad179bcf1397|
|DepartmentId| 需要调用接口获取的信息所在部门Id| 1c3464c8-6939-4b13-ba5d-45f44ed8b671|

#### 请求消息体

该部分可选。请求消息体通常以结构化格式（如JSON）发出，与请求消息头中Content-Type对应，传递除请求消息头之外的内容。
若请求消息体中的参数支持中文，则中文字符必须为UTF-8编码。


### 认证鉴权

#### 获取令牌

##### 功能介绍

获取用户访问令牌，用于接口调用

##### 基本信息

**Path：** /connect/token
**Method：** POST

**请求参考示例：**

*注意：Header中Content-Type应设置为application/x-www-form-urlencoded
client_id、grant_type为固定值，只需替换XXXXXX对应的字段内容，即：用户名及密码*

    client_id=thirdpartyservice&grant_type=password&username=XXXXXX&password=XXXXXX

**响应参考示例：**

    {
      Access_token: "this is a string",    // 访问令牌
      Expires_in: "this is a string",    // 令牌过期时间，单位秒
      Token_type: "this is a string",    // 令牌类型
      Refresh_token: "this is a string",    // 刷新令牌
      Scope: "this is a string",    // 令牌作用域
    }

#### 刷新令牌

##### 功能介绍

由于访问令牌生命周期有限，当访问令牌过期时，可使用刷新令牌请求新的访问令牌
**说明：刷新令牌仅能使用一次**


##### 基本信息

**Path：** /connect/token
**Method：** POST

**请求参考示例：**

*注意：Header中Content-Type应设置为application/x-www-form-urlencoded
client_id、grant_type为固定值，只需替换XXXXXX对应的字段内容，即：前一次请求（获取令牌/刷新令牌）的返回值*

    client_id=thirdpartyservice&grant_type=refresh_token&refresh_token=XXXXXX

**响应参考示例：**

    {
      Access_token: "this is a string",    // 访问令牌
      Expires_in: "this is a string",    // 令牌过期时间，单位秒
      Token_type: "this is a string",    // 令牌类型
      Refresh_token: "this is a string",    // 刷新令牌
      Scope: "this is a string",    // 令牌作用域
    }

---


### 返回结果

#### 响应消息头

响应消息头包含如下两部分：
● 一个HTTP状态代码，从2xx成功代码到4xx或5xx错误代码，或者可以返回服务定义的状态码。
● 附加响应头字段，如Content-Type响应消息头。
详细的公共响应消息头字段请参考下表
|名称| 描述| 示例|
|-|-|-|
|Content-Length| 服务端返回的消息实体的传输长度，以字节为单位| 3456|
|Content-type| 消息体的MIME类型| application/json|

#### 响应消息体

该部分可选。响应消息体通常以结构化格式（如JSON或XML）返回，与响应消息头中Content-Type对应，传递除响应消息头之外的内容。
以下是通用HTTP状态码说明
|HTTP状态码| 状态描述| 语义|
|-|-|-|
|200| OK| 请求成功|
|204| NoContent| 没有返回信息|
|400| BadRequest| 1．请求参数错误 2．因资源被引用而无法删除
|404| NotFound| 资源未找到
|415| Unsupported Media Type| 服务端未实现此请求方法，或要传递一个空的Json作为Body，即，”{}”
|500| InternalServerError| 服务器内部异常

## 业务接口列表

**业务请求应使用[公共请求消息头](#请求消息头)，若未特殊说明Content-Type为application/json**

### 公司

#### 查询用户所属公司列表

##### 功能介绍

根据条件查询用户所属公司列表

##### 基本信息

**Path：** /openapi/companies/companylist
**Method：** GET
**Query string：**

 | 参数名称|参数类型|描述|
|:-|:-|:-|
|Name|String|公司名称，模糊匹配|

**请求参考示例：**

无内容

**响应参考示例：**

    {
      Companies: [
        {
          CompanyUserId: "3d5b72cc-b2cd-42f8-bab3-177f602aab2b",    // 用户id
          CompanyUserName: "this is a string",    // 用户名称
          Id: "b3401024-cba6-41f4-8a76-d5c73afc74c7",    // 唯一标识
          Name: "this is a string",    // 名称
          Description: "this is a string",    // 备注
          Tags: ["this is a string","this is a string"],    // 标签
          Properties: {"Field1":"value1","Field2":"value2"},    // 附加属性
          CreatedAt: "2022-07-28T12:05:33.739Z",    // 创建时间
          CreatedBy: "87af034b-649a-4052-b778-0cb471d98a12",    // 创建人id
          CreatedByName: "this is a string",    // 创建人名称
          ModifiedBy: "1c65ba97-ab3b-4e94-b72b-8406529f0d80",    // 更新人id
          ModifiedByName: "this is a string",    // 更新人名称
          ModifiedAt: "2022-07-28T12:05:33.741Z",    // 更新时间
          CompanyId: "b70eaba5-591b-4083-b4f6-f39223552c80",    // 公司id
          Edition: "Enterprise",    // 版本类型:Enterprise-企业版;Community-社区版
        },
        {
          // 略，结构同前一节点
        }
      ],    // 公司列表
    }

---

### 部门

#### 获取当前公司部门树

##### 功能介绍

获取公司下整个部门树形层级结构

##### 基本信息

**Path：** /openapi/departments/tree
**Method：** GET

**请求参考示例：**

无内容

**响应参考示例：**

    {
      Count: 21,    // 查询命中总记录数
      RootDepartment: {
        Children: [
          {
            Children: { /* 略，结构同上级节点 */ },    // 子部门列表
            DepartmentPath: "this is a string",    // 部门树路径
            VisitorHasAnyRole: false,    // 用户是否在此部门中拥有角色
            VisitorHasPermission: false,    // 用户是否在此部门中拥有权限
            UserCount: 5,    // 用户数量
            Id: "80fc6b44-d571-43a0-8b97-19db66ea53d7",    // 唯一标识
            DepartmentId: "5c21c628-1fae-4e1b-b21b-87a254694647",    // 部门id
            ParentId: "3857f18d-fe7e-4306-ba89-d56eb3074a6b",    // parent id
            Name: "this is a string",    // 名称
            Description: "this is a string",    // 备注
            ETag: "this is a string",    // 版本控制标记
            Tags: ["this is a string","this is a string"],    // 标签
            Properties: {"Field1":"value1","Field2":"value2"},    // 附加属性
            CreatedAt: "2022-07-28T12:05:33.744Z",    // 创建时间
            ModifiedAt: "2022-07-28T12:05:33.744Z",    // 更新时间
            CompanyId: "43335e8e-8f90-45c2-b53f-0b56ea19f6d3",    // 公司id
          },
          {
            // 略，结构同前一节点
          }
        ],    // 子部门列表
        DepartmentPath: "this is a string",    // 部门树路径
        VisitorHasAnyRole: false,    // 用户是否在此部门中拥有角色
        VisitorHasPermission: false,    // 用户是否在此部门中拥有权限
        UserCount: 81,    // 用户数量
        Id: "31f3d66e-4a35-4ac4-bcd7-95aa9e90c475",    // 唯一标识
        DepartmentId: "1b36f19d-a068-4c74-a1f4-90de0a078475",    // 部门id
        ParentId: "9285c4eb-2cbe-41ca-bbbb-e00ed9694f59",    // parent id
        Name: "this is a string",    // 名称
        Description: "this is a string",    // 备注
        ETag: "this is a string",    // 版本控制标记
        Tags: ["this is a string","this is a string"],    // 标签
        Properties: {"Field1":"value1","Field2":"value2"},    // 附加属性
        CreatedAt: "2022-07-28T12:05:33.744Z",    // 创建时间
        ModifiedAt: "2022-07-28T12:05:33.744Z",    // 更新时间
        CompanyId: "ded22fda-3663-40a2-8310-78fd8a34db3e",    // 公司id
      },    // 部门树根节点
    }

---

### 流程包

#### 查询流程包列表

##### 功能介绍

根据参数查询对应的流程包列表

##### 基本信息

**Path：** /openapi/packages
**Method：** GET
**Query string：**

 | 参数名称|参数类型|描述|
|:-|:-|:-|
|IncludeVersions|Boolean|是否包含所有版本|
|SearchString|String|关键字，模糊匹配名称、备注、标签|
|Name|String|名称，模糊匹配|
|IsDesc|Boolean|是否按创建时间降序排序，默认true,说明:true-降序；false-升序|
|StartTime|DateTime|开始时间，格式:"2022-07-28T12:05:33.745Z"|
|EndTime|DateTime|结束时间，格式:"2022-07-28T12:05:33.745Z"|
|PageIndex|Int|页码（从0开始）|
|PageSize|Int|页大小（默认20）|

**请求参考示例：**

无内容

**响应参考示例：**

    {
      Count: 87,    // 查询命中总记录数
      List: [
        {
          Id: "e9102a90-c97c-4f57-8237-2bd804570de8",    // 唯一标识
          Name: "this is a string",    // 名称
          Description: "this is a string",    // 备注
          TotalDownloads: 77,    // 下载计数
          LastVersion: "this is a string",    // 最新版本号
          LastVersionId: "a662cc4c-f27d-4a51-bfeb-c6a9c9eb1d0d",    // 最高版本id
          CreatedAt: "2022-07-28T12:05:33.746Z",    // 创建时间
          ModifiedAt: "2022-07-28T12:05:33.746Z",    // 更新时间
          CreatedBy: "91750790-48a1-4b04-a13f-533305181477",    // 创建人id
          CreatedByName: "this is a string",    // 创建人名称
          ModifiedBy: "e38fbda2-60bd-4cb3-aa09-71a39caf31d1",    // 更新人id
          ModifiedByName: "this is a string",    // 更新人名称
          Tags: ["this is a string","this is a string"],    // 标签
          CompanyId: "73e8a001-dd2e-4be6-995d-908031371bd1",    // 公司id
          DepartmentId: "13bb89aa-bff1-442c-a639-702a28411533",    // 部门id
          IconUrl: "this is a string",    // 图标存放地址
          FullDescription: "this is a string",    // 备注详情
          Versions: [
            {
              Id: "f27b2b54-03ec-4a20-bee5-184b5bd8dd84",    // 唯一标识
              PackageId: "cc5c189d-7c07-426a-84c7-9e66eee5da2a",    // 流程包id
              Version: "this is a string",    // 机器人版本
              Description: "this is a string",    // 备注
              Arguments: [
                {
                  Name: "this is a string",    // 名称
                  Type: "this is a string",    // 参数类型
                  Direction: "In",    // 参数方向:In-入参;Out-出参;InOut-出入参
                  DefaultValue: "this is a string",    // 参数默认值; 当使用资产（即AssetContent不为null）时，这里为资产id
                  AssetContent: {
                    Name: "this is a string",    // 名称
                    Description: "this is a string",    // 备注
                    IsRequiredEncryption: false,    // 是否加密
                  },    // 使用的资产
                },
                {
                  // 略，结构同前一节点
                }
              ],    // 参数
              ETag: "this is a string",    // 版本控制标记
              UploadTime: "2022-07-28T12:05:33.746Z",    // 上传时间
              TotalDownloads: 8,    // 下载计数
              CreatedAt: "2022-07-28T12:05:33.746Z",    // 创建时间
              ModifiedAt: "2022-07-28T12:05:33.746Z",    // 更新时间
              CreatedBy: "195c7a65-4f65-41c2-bf62-7e5cf4de29c1",    // 创建人id
              CreatedByName: "this is a string",    // 创建人名称
              ModifiedBy: "7823876a-0f6f-4d26-b623-25a842ec9ff2",    // 更新人id
              ModifiedByName: "this is a string",    // 更新人名称
              CompanyId: "4db76bf1-182f-4d2f-a73d-ba83f39d93f0",    // 公司id
              DepartmentId: "1353f5a2-586d-4177-81ab-f516d330ee05",    // 部门id
              FullDescription: "this is a string",    // 备注详情
            },
            {
              // 略，结构同前一节点
            }
          ],    // 流程包所有版本
        },
        {
          // 略，结构同前一节点
        }
      ],    // 当前页记录集合
    }

#### 获取流程包详情

##### 功能介绍

根据packageId获取一个流程包具体的详细信息

##### 基本信息

**Path：** /openapi/packages/{packageId}
**Method：** GET

**请求参考示例：**

无内容

**响应参考示例：**

    {
      Id: "6f960449-cea4-4321-a242-109a22c64c55",    // 唯一标识
      Name: "this is a string",    // 名称
      Description: "this is a string",    // 备注
      TotalDownloads: 21,    // 下载计数
      LastVersion: "this is a string",    // 最新版本号
      LastVersionId: "116b0a4c-a999-46a9-b98c-e2d473d609f5",    // 最高版本id
      CreatedAt: "2022-07-28T12:05:33.746Z",    // 创建时间
      ModifiedAt: "2022-07-28T12:05:33.746Z",    // 更新时间
      CreatedBy: "1c3fcc7e-8d6c-45b3-a0a7-1600c880cf7d",    // 创建人id
      CreatedByName: "this is a string",    // 创建人名称
      ModifiedBy: "036c492d-15c3-4d4a-8643-39bed3e779a0",    // 更新人id
      ModifiedByName: "this is a string",    // 更新人名称
      Tags: ["this is a string","this is a string"],    // 标签
      CompanyId: "89cad849-bb8d-4bf3-af64-c048536f2d0d",    // 公司id
      DepartmentId: "c9b0e54c-95ec-46e7-9e40-4142b2d58d54",    // 部门id
      IconUrl: "this is a string",    // 图标存放地址
      FullDescription: "this is a string",    // 备注详情
      Versions: [
        {
          Id: "542b86e8-2235-4c77-90cc-dfe40aad0b8a",    // 唯一标识
          PackageId: "df19be76-b7aa-47b9-b5fa-b8bf70a314a3",    // 流程包id
          Version: "this is a string",    // 机器人版本
          Description: "this is a string",    // 备注
          Arguments: [
            {
              Name: "this is a string",    // 名称
              Type: "this is a string",    // 参数类型
              Direction: "In",    // 参数方向:In-入参;Out-出参;InOut-出入参
              DefaultValue: "this is a string",    // 参数默认值; 当使用资产（即AssetContent不为null）时，这里为资产id
              AssetContent: {
                Name: "this is a string",    // 名称
                Description: "this is a string",    // 备注
                IsRequiredEncryption: false,    // 是否加密
              },    // 使用的资产
            },
            {
              // 略，结构同前一节点
            }
          ],    // 参数
          ETag: "this is a string",    // 版本控制标记
          UploadTime: "2022-07-28T12:05:33.746Z",    // 上传时间
          TotalDownloads: 21,    // 下载计数
          CreatedAt: "2022-07-28T12:05:33.746Z",    // 创建时间
          ModifiedAt: "2022-07-28T12:05:33.746Z",    // 更新时间
          CreatedBy: "10723c21-796b-4839-b5be-3612bfc04e9e",    // 创建人id
          CreatedByName: "this is a string",    // 创建人名称
          ModifiedBy: "cd892d95-a33c-45b4-9257-e31709529d12",    // 更新人id
          ModifiedByName: "this is a string",    // 更新人名称
          CompanyId: "93f154af-eacf-431e-be54-c7ea764165a0",    // 公司id
          DepartmentId: "5d8a04e7-6dab-43e1-8b5e-5f03ab5e2b89",    // 部门id
          FullDescription: "this is a string",    // 备注详情
        },
        {
          // 略，结构同前一节点
        }
      ],    // 流程包所有版本
    }

#### 更新流程包

##### 功能介绍

根据流程包packageId更新流程包描述和标签

##### 基本信息

**Path：** /openapi/packages/{packageId}
**Method：** PATCH

**请求参考示例：**

    {
      ETag: "this is a string",    // 版本控制标记
      Tags: ["this is a string","this is a string"],    // 标签
    }
**响应参考示例：**

    {
      Id: "709d8707-a70d-4b5c-a498-8f64f5872169",    // 唯一标识
      Name: "this is a string",    // 名称
      Description: "this is a string",    // 备注
      TotalDownloads: 82,    // 下载计数
      LastVersion: "this is a string",    // 最新版本号
      LastVersionId: "411cc378-d03c-4b7f-9919-c9ba8fb672bb",    // 最高版本id
      CreatedAt: "2022-07-28T12:05:33.747Z",    // 创建时间
      ModifiedAt: "2022-07-28T12:05:33.747Z",    // 更新时间
      CreatedBy: "2d6ba6bd-c7cd-4425-a202-ad75cef630b6",    // 创建人id
      CreatedByName: "this is a string",    // 创建人名称
      ModifiedBy: "fb56aca7-4272-4c12-b336-727b4f9b12f0",    // 更新人id
      ModifiedByName: "this is a string",    // 更新人名称
      Tags: ["this is a string","this is a string"],    // 标签
      CompanyId: "29c965d5-aa49-4838-9a6f-6f36e36920e2",    // 公司id
      DepartmentId: "fb7ad308-7a96-47b1-97fc-f03945442521",    // 部门id
      IconUrl: "this is a string",    // 图标存放地址
      FullDescription: "this is a string",    // 备注详情
      Versions: [
        {
          Id: "7b09f834-de3c-4d8c-8f65-4c16a92977b3",    // 唯一标识
          PackageId: "b3da50e7-d7d0-41fd-a1b1-597674455727",    // 流程包id
          Version: "this is a string",    // 机器人版本
          Description: "this is a string",    // 备注
          Arguments: [
            {
              Name: "this is a string",    // 名称
              Type: "this is a string",    // 参数类型
              Direction: "In",    // 参数方向:In-入参;Out-出参;InOut-出入参
              DefaultValue: "this is a string",    // 参数默认值; 当使用资产（即AssetContent不为null）时，这里为资产id
              AssetContent: {
                Name: "this is a string",    // 名称
                Description: "this is a string",    // 备注
                IsRequiredEncryption: false,    // 是否加密
              },    // 使用的资产
            },
            {
              // 略，结构同前一节点
            }
          ],    // 参数
          ETag: "this is a string",    // 版本控制标记
          UploadTime: "2022-07-28T12:05:33.747Z",    // 上传时间
          TotalDownloads: 57,    // 下载计数
          CreatedAt: "2022-07-28T12:05:33.747Z",    // 创建时间
          ModifiedAt: "2022-07-28T12:05:33.747Z",    // 更新时间
          CreatedBy: "5440aa87-c28b-4bd2-9181-cab5a767388a",    // 创建人id
          CreatedByName: "this is a string",    // 创建人名称
          ModifiedBy: "b94ac2d3-e3bb-4187-825f-0f806094a447",    // 更新人id
          ModifiedByName: "this is a string",    // 更新人名称
          CompanyId: "396bdd5b-a690-4e33-babd-cd7e7f2d64d9",    // 公司id
          DepartmentId: "1302bf96-061d-4eef-a3a0-49412657f5ab",    // 部门id
          FullDescription: "this is a string",    // 备注详情
        },
        {
          // 略，结构同前一节点
        }
      ],    // 流程包所有版本
    }

#### 删除流程包

##### 功能介绍

根据packageId删除流程包

##### 基本信息

**Path：** /openapi/packages/{packageId}
**Method：** DELETE

**请求参考示例：**

无内容

**响应参考示例：**

无内容

#### 上传流程包并创建信息

##### 功能介绍

上传流程包文件并生成流程包版本信息。
注意：当流程包（.dgs 文件）大于 200M 时，创建流程包应按如下步骤实施：
1. 获取上传通道/地址，参见[【生成流程包上传地址】](#生成流程包上传地址)
2. 上传流程包 dgs 文件，参见[【通用上传文件流地址】](#通用上传文件流地址)
3. 通知完成上传，参见[【通知流程包上传完成】](#通知流程包上传完成)

##### 基本信息

**Path：** /openapi/packages
**Method：** PUT

**请求参考示例：**

*注意：请求头信息Content-Type应设置application/octet-stream*

    [二进制文件流数据]

**响应参考示例：**

    {
      PackageId: "28cfe85a-705c-4309-bea7-67525e7957aa",    // 流程包id
      PackageVersionId: "4ff4ccd0-8086-4bb8-9e30-58f2119e36e9",    // 流程包版本id
      PackageName: "this is a string",    // 流程包名称
      LastVersion: "this is a string",    // 最新版本号
    }

#### 生成流程包上传地址

##### 功能介绍

在不知道 dgs 信息的情况下创建上传通道

##### 基本信息

**Path：** /openapi/preloadedVersionFiles
**Method：** POST

**请求参考示例：**

无内容

**响应参考示例：**

    {
      Id: "9d3fe923-caea-4725-87a0-dbecaf37655d",    // 唯一标识
      Channel: {
        Uri: "this is a string",    // Url地址
        Headers: {"Content-Type":"application/octet-stream"},    // 请求Headers
      },    // 上传通道
      CreatedAt: "2022-07-28T12:05:33.747Z",    // 创建时间
      CompanyId: "97c2a0c5-558c-48ce-ba57-2bdbdfce02d5",    // 公司id
      DepartmentId: "d0c6b2e8-813f-4f70-9b31-879fdc851010",    // 部门id
    }

#### 通用上传文件流地址

##### 功能介绍

通过获取上传地址后将文件通过该接口上传至控制台

##### 基本信息

**Path：** 通过[【生成流程包上传地址】](#生成流程包上传地址)获取到的uri地址
**Method：** PUT

**请求参考示例：**

*注意：需将[【生成流程包上传地址】](#生成流程包上传地址)返回的headers数据以键值对方式填写在该接口的HTTP Headers中*

    [二进制文件流数据]

**响应参考示例：**

无内容

#### 通知流程包上传完成

##### 功能介绍

通过上传地址完成上传文件后调用该接口, 路径中id为[【生成流程包上传地址】](#生成流程包上传地址)返回的Id

##### 基本信息

**Path：** /openapi/preloadedVersionFiles/{id}
**Method：** PATCH

**请求参考示例：**

无内容

**响应参考示例：**

    {
      PackageId: "4708febb-5641-476c-bc64-0b8ce10b4f9e",    // 流程包id
      PackageVersionId: "0f00ad6d-512e-43e6-b58c-7955c65a9ceb",    // 流程包版本id
      PackageName: "this is a string",    // 流程包名称
      LastVersion: "this is a string",    // 最新版本号
    }

---

### 机器人组

#### 查询机器人组列表

##### 功能介绍

根据指定条件查询机器人组列表

##### 基本信息

**Path：** /openapi/queues
**Method：** GET
**Query string：**

 | 参数名称|参数类型|描述|
|:-|:-|:-|
|Name|String|名称，模糊匹配|
|SearchString|String|关键字，模糊匹配|
|IsDesc|Boolean|是否按创建时间降序排序，默认true,说明:true-降序；false-升序|
|AssociatedQueueId|String|机器人组id，格式:guid/uuid|
|PackageId|String|流程包id，格式:guid/uuid|
|PackageVersionId|String|流程包版本id，格式:guid/uuid|
|PageIndex|Int|页码（从0开始）|
|PageSize|Int|页大小（默认20）|

**请求参考示例：**

无内容

**响应参考示例：**

    {
      Count: 36,    // 查询命中总记录数
      List: [
        {
          Id: "9e39d732-90cd-46f8-8176-d6df5c34bc97",    // 唯一标识
          Name: "this is a string",    // 名称
          Description: "this is a string",    // 备注
          Tags: ["this is a string","this is a string"],    // 标签
          Properties: {"Field1":"value1","Field2":"value2"},    // 附加属性
          CreatedAt: "2022-07-28T12:05:33.748Z",    // 创建时间
          CreatedBy: "4f1a5a8a-2e46-41f7-8f99-ec0715ce122c",    // 创建人id
          CreatedByName: "this is a string",    // 创建人名称
          ModifiedAt: "2022-07-28T12:05:33.748Z",    // 更新时间
          ModifiedBy: "66495839-cd53-4b44-899c-5544fc178679",    // 更新人id
          ModifiedByName: "this is a string",    // 更新人名称
          RobotCount: 5,    // 包含机器人数量
          CompanyId: "13f5872f-7a19-45d5-ad6a-ba2fc29ad8df",    // 公司id
          DepartmentId: "3e9fc787-3c87-44f8-8d97-3211d467ff5b",    // 部门id
        },
        {
          // 略，结构同前一节点
        }
      ],    // 当前页记录集合
    }

#### 根据id获取机器人组信息

##### 功能介绍

根据Id获取机器人组

##### 基本信息

**Path：** /openapi/queues/{queueId}
**Method：** GET

**请求参考示例：**

无内容

**响应参考示例：**

    {
      Id: "ada12915-bb95-4d5d-addd-ffa0ac04b3e9",    // 唯一标识
      Name: "this is a string",    // 名称
      Description: "this is a string",    // 备注
      Tags: ["this is a string","this is a string"],    // 标签
      Properties: {"Field1":"value1","Field2":"value2"},    // 附加属性
      CreatedAt: "2022-07-28T12:05:33.748Z",    // 创建时间
      CreatedBy: "37d716c2-5b8e-4459-88c3-adc09b2ed61e",    // 创建人id
      CreatedByName: "this is a string",    // 创建人名称
      ModifiedAt: "2022-07-28T12:05:33.748Z",    // 更新时间
      ModifiedBy: "d9494330-dff2-48ce-b88e-20e4392d1ebf",    // 更新人id
      ModifiedByName: "this is a string",    // 更新人名称
      RobotCount: 8,    // 包含机器人数量
      CompanyId: "9d394886-5643-4465-83d1-6107d54ee50b",    // 公司id
      DepartmentId: "d2f08316-de9a-40ec-9e4a-1b2261fd97ee",    // 部门id
    }

#### 删除机器人组

##### 功能介绍

根据Id删除机器人组

##### 基本信息

**Path：** /openapi/queues/{queueId}
**Method：** DELETE

**请求参考示例：**

无内容

**响应参考示例：**

无内容

#### 更新机器人组信息

##### 功能介绍

根据Id更新机器人组信息

##### 基本信息

**Path：** /openapi/queues/{queueId}
**Method：** PATCH

**请求参考示例：**

    {
      Name: "this is a string",    // 名称
      Description: "this is a string",    // 备注
      Tags: ["this is a string","this is a string"],    // 标签
    }
**响应参考示例：**

    {
      Id: "e2c8ad94-b4d5-4c9d-8f9c-2f337f941421",    // 唯一标识
      Name: "this is a string",    // 名称
      Description: "this is a string",    // 备注
      Tags: ["this is a string","this is a string"],    // 标签
      Properties: {"Field1":"value1","Field2":"value2"},    // 附加属性
      CreatedAt: "2022-07-28T12:05:33.748Z",    // 创建时间
      CreatedBy: "a42b2c74-576a-4514-8128-04db2af9b42c",    // 创建人id
      CreatedByName: "this is a string",    // 创建人名称
      ModifiedAt: "2022-07-28T12:05:33.748Z",    // 更新时间
      ModifiedBy: "0273c74d-fb6a-491f-9787-78e329576871",    // 更新人id
      ModifiedByName: "this is a string",    // 更新人名称
      RobotCount: 98,    // 包含机器人数量
      CompanyId: "b5b63ad9-8ad4-4395-8c48-9192b602e34f",    // 公司id
      DepartmentId: "7603dc97-701c-46d3-9c3e-b60c3f0a242c",    // 部门id
    }

#### 创建机器人组

##### 功能介绍

创建机器人组

##### 基本信息

**Path：** /openapi/queues/{queueId}
**Method：** POST

**请求参考示例：**

    {
      Name: "this is a string",    // 名称
      Description: "this is a string",    // 备注
      Tags: ["this is a string","this is a string"],    // 标签
      CompanyId: "882e681a-802f-4a41-9a7c-ea4d02c833f8",    // 公司id
      DepartmentId: "457315de-bf51-4112-bb85-e7664855bc23",    // 部门id
      CreateBy: "ed35ad95-253b-4f68-90af-2045c8b01505",    // 创建人id
      CreateByName: "this is a string",    // 创建人名称
    }
**响应参考示例：**

    {
      Id: "03f691f5-9c80-4bb8-ba50-3ab2f6916f33",    // 唯一标识
      Name: "this is a string",    // 名称
      Description: "this is a string",    // 备注
      Tags: ["this is a string","this is a string"],    // 标签
      Properties: {"Field1":"value1","Field2":"value2"},    // 附加属性
      CreatedAt: "2022-07-28T12:05:33.748Z",    // 创建时间
      CreatedBy: "27b9ba56-1201-477a-8099-ccf2372cb909",    // 创建人id
      CreatedByName: "this is a string",    // 创建人名称
      ModifiedAt: "2022-07-28T12:05:33.748Z",    // 更新时间
      ModifiedBy: "db01383d-49b0-4c49-a370-e20fee85712d",    // 更新人id
      ModifiedByName: "this is a string",    // 更新人名称
      RobotCount: 19,    // 包含机器人数量
      CompanyId: "1d1359ee-1dfd-4549-9420-1c66e3e432d9",    // 公司id
      DepartmentId: "495895d1-e52e-49ba-95ca-3cfb22f0b607",    // 部门id
    }

---

### 机器人

#### 查询机器人列表

##### 功能介绍

根据指定查询条件，获取机器人列表

##### 基本信息

**Path：** /openapi/robots
**Method：** GET
**Query string：**

 | 参数名称|参数类型|描述|
|:-|:-|:-|
|Name|String|名称，模糊匹配|
|SearchString|String|关键字，模糊匹配|
|IsDesc|Boolean|是否按创建时间降序排序，默认true,说明:true-降序；false-升序|
|AssociatedQueueId|String|机器人组id，格式:guid/uuid|
|PackageId|String|流程包id，格式:guid/uuid|
|PackageVersionId|String|流程包版本id，格式:guid/uuid|
|PageIndex|Int|页码（从0开始）|
|PageSize|Int|页大小（默认20）|

**请求参考示例：**

无内容

**响应参考示例：**

    {
      Count: 42,    // 查询命中总记录数
      List: [
        {
          Id: "ccd67183-1a1e-4911-a5f5-5b16d7326289",    // 唯一标识
          CurrentDeviceId: "9f2ab6aa-6b7e-4612-a466-872bee23a87e",    // 当前所在设备的id
          LastHeartbeatTime: "2022-07-28T12:05:33.749Z",    // 最近心跳时间
          ListeningStatus: ,    // 许可状态
          CompanyId: "03fb405f-7208-4c07-9b5f-631cefe87a3f",    // 公司id
          DepartmentId: "0532a285-08dd-40f0-b7c6-44505ff42dc4",    // 部门id
          SdkVersion: "this is a string",    // 机器人sdk版本
          Name: "this is a string",    // 名称
          Description: "this is a string",    // 备注
          CanUpgrade: false,    // 是否可升级
          LicenseStatus: "Unlicensed",    // 许可状态:Unlicensed-未许可;ClientLicensed-本地许可;ServerLicensed-控制台许可;PoolLicensed-浮动许可
          ClientSku: "this is a string",    // 机器人许可类型
          Version: "this is a string",    // 机器人版本
          Status: "Disconnected",    // 机器人状态:Disconnected-已断开;Ready-空闲;Busy-忙碌
        },
        {
          // 略，结构同前一节点
        }
      ],    // 当前页记录集合
    }

#### 获取机器人详情

##### 功能介绍

根据id获取机器人详情

##### 基本信息

**Path：** /openapi/robots/{robotId}
**Method：** GET

**请求参考示例：**

无内容

**响应参考示例：**

    {
      Id: "0971bc4c-7e7f-43da-b772-9f47c0dd38f9",    // 唯一标识
      CurrentDeviceId: "3cd9a994-8d94-4030-b443-a7e1fe2b3b18",    // 当前所在设备的id
      LastHeartbeatTime: "2022-07-28T12:05:33.749Z",    // 最近心跳时间
      ListeningStatus: ,    // 许可状态
      CompanyId: "09824b4b-34ee-430c-9f30-71dd806da925",    // 公司id
      DepartmentId: "62c0cf78-2e99-4a67-9136-27da0b6fbb07",    // 部门id
      SdkVersion: "this is a string",    // 机器人sdk版本
      Name: "this is a string",    // 名称
      Description: "this is a string",    // 备注
      CanUpgrade: false,    // 是否可升级
      LicenseStatus: "Unlicensed",    // 许可状态:Unlicensed-未许可;ClientLicensed-本地许可;ServerLicensed-控制台许可;PoolLicensed-浮动许可
      ClientSku: "this is a string",    // 机器人许可类型
      Version: "this is a string",    // 机器人版本
      Status: "Disconnected",    // 机器人状态:Disconnected-已断开;Ready-空闲;Busy-忙碌
    }

---

### 用户

#### 查询当前用户信息

##### 功能介绍

获取当前公司用户自己的信息

##### 基本信息

**Path：** /openapi/companyusers/self
**Method：** GET

**请求参考示例：**

无内容

**响应参考示例：**

    {
      UserExtensionInfo: {
        ExtensionId: "this is a string",    // 扩展字段-id
        ExtensionMobile: "this is a string",    // 扩展字段-手机号
        ExtensionEmail: "this is a string",    // 扩展字段-邮箱
      },    // user extensiong info
      IsExternalUser: false,    // is third party user
      HasModifiedPassword: false,    // 
      Id: "c9ea3614-6a20-4d2d-bdf0-3fbdf8131e0b",    // 唯一标识
      UserId: "78c69a10-86bc-40d6-8e04-6e850392bcf1",    // user id
      CompanyId: "b8d85209-bed1-4d05-810e-fe29c145bcfb",    // 公司id
      IsCompanyOwner: false,    // is company owner
      RoleIds: ["7c4ef050-2080-41a1-ad7d-ca5d4ebefcad","a4444c18-47a4-4663-8aab-1244542b895c"],    // roleIds
      Status: "Active",    // 机器人状态:Active-活跃;Inactive-未激活;Banned-禁用
      ResourceType: "Company",    // resource type:Company;Department;CompanyUser;Robot;Workflow;Queue;RunInstance;Dashboard;DataQueue;Asset;App;AppVersion;DataConnection;DataSource;SamplePackage;Package;PackageVersion;RpaLicenseBinding;TaskQueue
      AssignedDepartmentId: "36c933bf-bb00-49e6-9413-e9fdd889c5b8",    // 当前选中部门id
      ParentResourceType: "Company",    // resource type of parent:Company;Department;CompanyUser;Robot;Workflow;Queue;RunInstance;Dashboard;DataQueue;Asset;App;AppVersion;DataConnection;DataSource;SamplePackage;Package;PackageVersion;RpaLicenseBinding;TaskQueue
      ParentId: "f254c5d1-2208-41b8-8a37-03f8f16edb78",    // parent id
      Name: "this is a string",    // 名称
      Job: "this is a string",    // Job of the company user
      Description: "this is a string",    // 备注
      ETag: "this is a string",    // 版本控制标记
      Tags: ["this is a string","this is a string"],    // 标签
      Properties: {"Field1":"value1","Field2":"value2"},    // 附加属性
      CreatedAt: "2022-07-28T12:05:33.750Z",    // 创建时间
      ModifiedAt: "2022-07-28T12:05:33.750Z",    // 更新时间
      LastLoginAt: "2022-07-28T12:05:33.750Z",    // 最近登录时间
      DepartmentPath: "this is a string",    // 部门树路径
      Email: "this is a string",    // 邮箱
      PhoneNumber: "this is a string",    // user phone number
      UserName: "this is a string",    // account user name
    }

---

### 流程部署

#### 查询流程部署列表

##### 功能介绍

根据指定查询条件，获取流程部署列表

##### 基本信息

**Path：** /openapi/workflows
**Method：** GET
**Query string：**

 | 参数名称|参数类型|描述|
|:-|:-|:-|
|Name|String|名称，模糊匹配|
|SearchString|String|关键字，模糊匹配|
|IsDesc|Boolean|是否按创建时间降序排序，默认true,说明:true-降序；false-升序|
|AssociatedQueueId|String|机器人组id，格式:guid/uuid|
|PackageId|String|流程包id，格式:guid/uuid|
|PackageVersionId|String|流程包版本id，格式:guid/uuid|
|PageIndex|Int|页码（从0开始）|
|PageSize|Int|页大小（默认20）|

**请求参考示例：**

无内容

**响应参考示例：**

    {
      Count: 96,    // 查询命中总记录数
      List: [
        {
          Id: "0b25d34b-7127-4d2f-894a-54c096bc33ab",    // 唯一标识
          DepartmentId: "652d9dc0-80bf-4fe9-babe-47104eda883a",    // 部门id
          Name: "this is a string",    // 名称
          Description: "this is a string",    // 备注
          Arguments: [
            {
              Name: "this is a string",    // 名称
              Type: "this is a string",    // 参数类型
              Direction: "In",    // 参数方向:In-入参;Out-出参;InOut-出入参
              DefaultValue: "this is a string",    // 参数默认值; 当使用资产（即AssetContent不为null）时，这里为资产id
              AssetContent: {
                Name: "this is a string",    // 名称
                Description: "this is a string",    // 备注
                IsRequiredEncryption: false,    // 是否加密
              },    // 使用的资产
            },
            {
              // 略，结构同前一节点
            }
          ],    // 参数
          Tags: ["this is a string","this is a string"],    // 标签
          VideoRecordMode: "NeverRecord",    // 视频录制模式:NeverRecord-从不录制;ReportOnlyWhenSucceeded-仅成功时上传;ReportOnlyWhenFailed-仅失败时上传;AlwaysRecord-总是录制;AlwaysReport-总是上传
          Properties: {"Field1":"value1","Field2":"value2"},    // 附加属性
          JobStateNotification: {
            SwitchOn: false,    // 通知功能是否启用
            JustNoticeAgainstLastRuninstance: false,    // 通知功能是否启用
            Conditions: ,    // 通知条件
            Recipients: ["this is a string","this is a string"],    // 邮件接收者
          },    // 运行状态通知
          PackageName: "this is a string",    // 流程包名称
          PackageId: "7e312ea0-4c9a-46c9-85da-67cec66f7638",    // 流程包id
          PackageVersionId: "ca6c565a-6864-4033-a7fc-d3e6ebce4b7a",    // 流程包版本id
          PackageVersion: "this is a string",    // 流程包版本号
          AssociatedQueueId: "881e2f9b-5370-479b-b344-f7596355a2fd",    // （执行目标）机器人组id
          Priority: 2000,    // 优先级，范围:0-5000
          MaxRetryCount: 1,    // 最大重试次数,范围:0-10
          TriggerPolicy: "SkipWhenNoResource",    // 执行策略:SkipWhenNoResource-无可用机器人时取消任务;AnyTime-无可用机器人时等待
          LastTriggeredAt: "2022-07-28T12:05:33.751Z",    // 最后触发时间
          TriggeredCount: 20,    // 触发计数
          CreatedAt: "2022-07-28T12:05:33.751Z",    // 创建时间
          CreatedBy: "0bdcb832-b91e-4325-afe4-23b9eddfea7b",    // 创建人id
          CreatedByName: "this is a string",    // 创建人名称
          ModifiedAt: "2022-07-28T12:05:33.751Z",    // 更新时间
          ModifiedBy: "8ca68f29-820e-42ef-bbb2-a5e7f8d913c2",    // 更新人id
          ModifiedByName: "this is a string",    // 更新人名称
          CompanyId: "8b377286-db01-4d31-b368-a391d76a23e0",    // 公司id
        },
        {
          // 略，结构同前一节点
        }
      ],    // 当前页记录集合
    }

#### 获取流程部署详情

##### 功能介绍

根据id获取流程部署详情

##### 基本信息

**Path：** /openapi/workflows/{workflowId}
**Method：** GET

**请求参考示例：**

无内容

**响应参考示例：**

    {
      Id: "04e7b276-5644-47ae-9e47-9c21e2df2f7b",    // 唯一标识
      DepartmentId: "0e97951e-73fc-4836-9a6c-eb9e8c032db1",    // 部门id
      Name: "this is a string",    // 名称
      Description: "this is a string",    // 备注
      Arguments: [
        {
          Name: "this is a string",    // 名称
          Type: "this is a string",    // 参数类型
          Direction: "In",    // 参数方向:In-入参;Out-出参;InOut-出入参
          DefaultValue: "this is a string",    // 参数默认值; 当使用资产（即AssetContent不为null）时，这里为资产id
          AssetContent: {
            Name: "this is a string",    // 名称
            Description: "this is a string",    // 备注
            IsRequiredEncryption: false,    // 是否加密
          },    // 使用的资产
        },
        {
          // 略，结构同前一节点
        }
      ],    // 参数
      Tags: ["this is a string","this is a string"],    // 标签
      VideoRecordMode: "NeverRecord",    // 视频录制模式:NeverRecord-从不录制;ReportOnlyWhenSucceeded-仅成功时上传;ReportOnlyWhenFailed-仅失败时上传;AlwaysRecord-总是录制;AlwaysReport-总是上传
      Properties: {"Field1":"value1","Field2":"value2"},    // 附加属性
      JobStateNotification: {
        SwitchOn: false,    // 通知功能是否启用
        JustNoticeAgainstLastRuninstance: false,    // 通知功能是否启用
        Conditions: ,    // 通知条件
        Recipients: ["this is a string","this is a string"],    // 邮件接收者
      },    // 运行状态通知
      PackageName: "this is a string",    // 流程包名称
      PackageId: "92c0cee8-c1cc-4c48-b9df-a79310cf2d23",    // 流程包id
      PackageVersionId: "95fa3edc-4b37-450e-8ad6-cd820102430b",    // 流程包版本id
      PackageVersion: "this is a string",    // 流程包版本号
      AssociatedQueueId: "3609d439-b12e-4a8c-bb28-485a5eab71e7",    // （执行目标）机器人组id
      Priority: 2000,    // 优先级，范围:0-5000
      MaxRetryCount: 1,    // 最大重试次数,范围:0-10
      TriggerPolicy: "SkipWhenNoResource",    // 执行策略:SkipWhenNoResource-无可用机器人时取消任务;AnyTime-无可用机器人时等待
      LastTriggeredAt: "2022-07-28T12:05:33.751Z",    // 最后触发时间
      TriggeredCount: 59,    // 触发计数
      CreatedAt: "2022-07-28T12:05:33.751Z",    // 创建时间
      CreatedBy: "5a77f0c8-45da-4b36-9e56-c02f804c19cd",    // 创建人id
      CreatedByName: "this is a string",    // 创建人名称
      ModifiedAt: "2022-07-28T12:05:33.751Z",    // 更新时间
      ModifiedBy: "0d8bfb0b-e385-42b8-9aa0-29f59590130e",    // 更新人id
      ModifiedByName: "this is a string",    // 更新人名称
      CompanyId: "74ac88a5-8bcb-4bf3-b11c-937af91fb8bd",    // 公司id
    }

#### 创建流程部署

##### 功能介绍

创建流程部署

##### 基本信息

**Path：** /openapi/workflows
**Method：** POST

**请求参考示例：**

    {
      Name: "this is a string",    // 名称
      Description: "this is a string",    // 备注
      Tags: ["this is a string","this is a string"],    // 标签
      Arguments: [
        {
          Name: "this is a string",    // 名称
          Type: "this is a string",    // 参数类型
          Direction: "In",    // 参数方向:In-入参;Out-出参;InOut-出入参
          DefaultValue: "this is a string",    // 参数默认值; 当使用资产（即AssetContent不为null）时，这里为资产id
          AssetContent: {
            Name: "this is a string",    // 名称
            Description: "this is a string",    // 备注
            IsRequiredEncryption: false,    // 是否加密
          },    // 使用的资产
        },
        {
          // 略，结构同前一节点
        }
      ],    // 参数
      VideoRecordMode: "NeverRecord",    // 视频录制模式:NeverRecord-从不录制;ReportOnlyWhenSucceeded-仅成功时上传;ReportOnlyWhenFailed-仅失败时上传;AlwaysRecord-总是录制;AlwaysReport-总是上传
      JobStateNotification: {
        SwitchOn: false,    // 通知功能是否启用
        JustNoticeAgainstLastRuninstance: false,    // 通知功能是否启用
        Conditions: ,    // 通知条件
        Recipients: ["this is a string","this is a string"],    // 邮件接收者
      },    // 运行状态通知
      PackageName: "this is a string",    // 流程包名称
      PackageId: "50d17f1e-7b18-4746-914e-acbf639bb583",    // 流程包id
      PackageVersionId: "e7f7495c-1e1d-4487-b62a-8d13f2ca6818",    // 流程包版本id
      PackageVersion: "this is a string",    // 流程包版本号
      AssociatedQueueId: "38a5b6e5-3aff-4e5a-8f41-0c99f909a6dc",    // （执行目标）机器人组id
      Priority: 2000,    // 优先级，范围:0-5000
      MaxRetryCount: 1,    // 最大重试次数,范围:0-10
      TriggerPolicy: "SkipWhenNoResource",    // 执行策略:SkipWhenNoResource-无可用机器人时取消任务;AnyTime-无可用机器人时等待
    }
**响应参考示例：**

    {
      Id: "f968a45c-e4d9-4c5a-aee5-141e37928bb1",    // 唯一标识
      DepartmentId: "1b3d9830-4b28-406d-bec1-de9f4fbf30e1",    // 部门id
      Name: "this is a string",    // 名称
      Description: "this is a string",    // 备注
      Arguments: [
        {
          Name: "this is a string",    // 名称
          Type: "this is a string",    // 参数类型
          Direction: "In",    // 参数方向:In-入参;Out-出参;InOut-出入参
          DefaultValue: "this is a string",    // 参数默认值; 当使用资产（即AssetContent不为null）时，这里为资产id
          AssetContent: {
            Name: "this is a string",    // 名称
            Description: "this is a string",    // 备注
            IsRequiredEncryption: false,    // 是否加密
          },    // 使用的资产
        },
        {
          // 略，结构同前一节点
        }
      ],    // 参数
      Tags: ["this is a string","this is a string"],    // 标签
      VideoRecordMode: "NeverRecord",    // 视频录制模式:NeverRecord-从不录制;ReportOnlyWhenSucceeded-仅成功时上传;ReportOnlyWhenFailed-仅失败时上传;AlwaysRecord-总是录制;AlwaysReport-总是上传
      Properties: {"Field1":"value1","Field2":"value2"},    // 附加属性
      JobStateNotification: {
        SwitchOn: false,    // 通知功能是否启用
        JustNoticeAgainstLastRuninstance: false,    // 通知功能是否启用
        Conditions: ,    // 通知条件
        Recipients: ["this is a string","this is a string"],    // 邮件接收者
      },    // 运行状态通知
      PackageName: "this is a string",    // 流程包名称
      PackageId: "3028ea42-f535-46d6-b464-bd9d971434b7",    // 流程包id
      PackageVersionId: "60a47924-3664-4da6-be3e-6577b7f4a7ca",    // 流程包版本id
      PackageVersion: "this is a string",    // 流程包版本号
      AssociatedQueueId: "24db2862-91be-47ea-9c9b-261536026dcb",    // （执行目标）机器人组id
      Priority: 2000,    // 优先级，范围:0-5000
      MaxRetryCount: 1,    // 最大重试次数,范围:0-10
      TriggerPolicy: "SkipWhenNoResource",    // 执行策略:SkipWhenNoResource-无可用机器人时取消任务;AnyTime-无可用机器人时等待
      LastTriggeredAt: "2022-07-28T12:05:33.751Z",    // 最后触发时间
      TriggeredCount: 35,    // 触发计数
      CreatedAt: "2022-07-28T12:05:33.751Z",    // 创建时间
      CreatedBy: "fae8bb08-9384-4eb9-8cf9-eb80e8f8dc35",    // 创建人id
      CreatedByName: "this is a string",    // 创建人名称
      ModifiedAt: "2022-07-28T12:05:33.751Z",    // 更新时间
      ModifiedBy: "0b7be5ef-6956-4978-9fef-08b535f7cd16",    // 更新人id
      ModifiedByName: "this is a string",    // 更新人名称
      CompanyId: "3e1ad626-0e4b-48fc-a35b-9213011f62d2",    // 公司id
    }

#### 更新流程部署

##### 功能介绍

根据id更新流程部署详情

##### 基本信息

**Path：** /openapi/workflows/{workflowId}
**Method：** PATCH

**请求参考示例：**

    {
      Name: "this is a string",    // 名称
      Description: "this is a string",    // 备注
      Arguments: [
        {
          Name: "this is a string",    // 名称
          Type: "this is a string",    // 参数类型
          Direction: "In",    // 参数方向:In-入参;Out-出参;InOut-出入参
          DefaultValue: "this is a string",    // 参数默认值; 当使用资产（即AssetContent不为null）时，这里为资产id
          AssetContent: {
            Name: "this is a string",    // 名称
            Description: "this is a string",    // 备注
            IsRequiredEncryption: false,    // 是否加密
          },    // 使用的资产
        },
        {
          // 略，结构同前一节点
        }
      ],    // 参数
      Tags: ["this is a string","this is a string"],    // 标签
      Properties: {"Field1":"value1","Field2":"value2"},    // 附加属性
      VideoRecordMode: ,    // 视频录制模式
      JobStateNotification: {
        SwitchOn: false,    // 通知功能是否启用
        JustNoticeAgainstLastRuninstance: false,    // 通知功能是否启用
        Conditions: ,    // 通知条件
        Recipients: ["this is a string","this is a string"],    // 邮件接收者
      },    // 运行状态通知
      TriggerPolicy: ,    // 执行策略
      PackageName: "this is a string",    // 流程包名称
      PackageVersionId: "3ccfdffb-9eda-48d0-9136-89c58ba2568a",    // 流程包版本id
      PackageVersion: "this is a string",    // 流程包版本号
      AssociatedQueueId: "2e511210-1b1d-4f0f-b17e-7e170e30d69d",    // （执行目标）机器人组id
      Priority: 2000,    // 优先级，范围:0-5000
      MaxRetryCount: 1,    // 最大重试次数,范围:0-10
    }
**响应参考示例：**

    {
      Id: "e85e2252-02d9-41a4-a615-7b1998dadc1d",    // 唯一标识
      DepartmentId: "259e3f06-3c5d-4d55-95ff-8a0500440462",    // 部门id
      Name: "this is a string",    // 名称
      Description: "this is a string",    // 备注
      Arguments: [
        {
          Name: "this is a string",    // 名称
          Type: "this is a string",    // 参数类型
          Direction: "In",    // 参数方向:In-入参;Out-出参;InOut-出入参
          DefaultValue: "this is a string",    // 参数默认值; 当使用资产（即AssetContent不为null）时，这里为资产id
          AssetContent: {
            Name: "this is a string",    // 名称
            Description: "this is a string",    // 备注
            IsRequiredEncryption: false,    // 是否加密
          },    // 使用的资产
        },
        {
          // 略，结构同前一节点
        }
      ],    // 参数
      Tags: ["this is a string","this is a string"],    // 标签
      VideoRecordMode: "NeverRecord",    // 视频录制模式:NeverRecord-从不录制;ReportOnlyWhenSucceeded-仅成功时上传;ReportOnlyWhenFailed-仅失败时上传;AlwaysRecord-总是录制;AlwaysReport-总是上传
      Properties: {"Field1":"value1","Field2":"value2"},    // 附加属性
      JobStateNotification: {
        SwitchOn: false,    // 通知功能是否启用
        JustNoticeAgainstLastRuninstance: false,    // 通知功能是否启用
        Conditions: ,    // 通知条件
        Recipients: ["this is a string","this is a string"],    // 邮件接收者
      },    // 运行状态通知
      PackageName: "this is a string",    // 流程包名称
      PackageId: "fcb438e6-5b8a-4fe9-a380-4cbcdc87639d",    // 流程包id
      PackageVersionId: "5f119266-7148-4798-bf46-6d1dd31a0037",    // 流程包版本id
      PackageVersion: "this is a string",    // 流程包版本号
      AssociatedQueueId: "499b52ad-1067-4297-8ae6-ee64d3ff6d59",    // （执行目标）机器人组id
      Priority: 2000,    // 优先级，范围:0-5000
      MaxRetryCount: 1,    // 最大重试次数,范围:0-10
      TriggerPolicy: "SkipWhenNoResource",    // 执行策略:SkipWhenNoResource-无可用机器人时取消任务;AnyTime-无可用机器人时等待
      LastTriggeredAt: "2022-07-28T12:05:33.752Z",    // 最后触发时间
      TriggeredCount: 65,    // 触发计数
      CreatedAt: "2022-07-28T12:05:33.752Z",    // 创建时间
      CreatedBy: "a016d477-4dfe-4e86-82bb-c58534188537",    // 创建人id
      CreatedByName: "this is a string",    // 创建人名称
      ModifiedAt: "2022-07-28T12:05:33.752Z",    // 更新时间
      ModifiedBy: "cb03028e-fae4-4778-b125-010f16e3512f",    // 更新人id
      ModifiedByName: "this is a string",    // 更新人名称
      CompanyId: "f8950bb9-c79a-4a3e-aa61-1563a8b8cbf4",    // 公司id
    }

#### 删除流程部署

##### 功能介绍

根据id删除流程部署

##### 基本信息

**Path：** /openapi/workflows/{workflowId}
**Method：** DELETE

**请求参考示例：**

无内容

**响应参考示例：**

无内容

#### 执行流程部署

##### 功能介绍

根据id执行流程部署

##### 基本信息

**Path：** /openapi/workflows/{workflowId}/execute
**Method：** POST

**请求参考示例：**

    {
      Source: "External",    // 来源:External-外部系统
      Arguments: [
        {
          Name: "this is a string",    // 名称
          Type: "this is a string",    // 参数类型
          Direction: "In",    // 参数方向:In-入参;Out-出参;InOut-出入参
          DefaultValue: "this is a string",    // 参数默认值; 当使用资产（即AssetContent不为null）时，这里为资产id
          AssetContent: {
            Name: "this is a string",    // 名称
            Description: "this is a string",    // 备注
            IsRequiredEncryption: false,    // 是否加密
          },    // 使用的资产
        },
        {
          // 略，结构同前一节点
        }
      ],    // 参数
      VideoRecordMode: ,    // 视频录制模式
      Priority: 2000,    // 优先级，范围:0-5000
      MaxRetryCount: 1,    // 最大重试次数,范围:0-10
      StartupType: ,    // 启动类型
      StartupId: "0e119955-e9f1-4394-8755-8c9b5cdf4814",    // 启动资源Id
      StartupName: "this is a string",    // 启动资源名
      JobSubjectId: "80758958-b039-437b-bf3b-d32a38f053cf",    // Job关联体id
      JobSubjectName: "this is a string",    // Job关联体名称
      JobSubjectType: "this is a string",    // Job关联体类型
    }
**响应参考示例：**

    [
      {
        Id: "b79541a7-c35b-43b4-a671-843655c0f10f",    // 唯一标识
        WorkflowId: "5256c3aa-cdf4-40e7-b2c5-cf49e801317d",    // 流程部署id
        Name: "this is a string",    // 名称
        Description: "this is a string",    // 备注
        PackageId: "066a7aef-f61a-40f6-9ade-9344a68ecd59",    // 流程包id
        PackageName: "this is a string",    // 流程包名称
        PackageVersion: "this is a string",    // 流程包版本号
        PackageVersionId: "7510d7c8-eefe-4410-8448-a9c97b2d7982",    // 流程包版本id
        Arguments: [
          {
            Name: "this is a string",    // 名称
            Type: "this is a string",    // 参数类型
            Direction: "In",    // 参数方向:In-入参;Out-出参;InOut-出入参
            DefaultValue: "this is a string",    // 参数默认值; 当使用资产（即AssetContent不为null）时，这里为资产id
            AssetContent: {
              Name: "this is a string",    // 名称
              Description: "this is a string",    // 备注
              IsRequiredEncryption: false,    // 是否加密
            },    // 使用的资产
          },
          {
            // 略，结构同前一节点
          }
        ],    // 参数
        ContainingQueueId: "3c8d6fe5-fa46-4a87-8f8a-e23f6ee8348f",    // 
        Priority: 2000,    // 优先级，范围:0-5000
        State: "Queued",    // :Queued-等待中;Running-运行中;Failed-失败;Cancelled-已取消;Succeeded-成功;Paused-已暂停
        DisplayState: "Queued",    // :Queued-等待中;Running-运行中;Failed-失败;Cancelled-已取消;Succeeded-成功;Paused-已暂停
        VideoRecordMode: "NeverRecord",    // 视频录制模式:NeverRecord-从不录制;ReportOnlyWhenSucceeded-仅成功时上传;ReportOnlyWhenFailed-仅失败时上传;AlwaysRecord-总是录制;AlwaysReport-总是上传
        Message: "this is a string",    // 原因说明
        MaxRunSeconds: 35,    // 
        Deleted: false,    // 
        LastRunInstaceId: "a312ca3a-8a15-42ba-a1ca-dbaacd256a9d",    // 
        LastRobotName: "this is a string",    // 
        StartedAt: "2022-07-28T12:05:33.753Z",    // 
        FinishedAt: "2022-07-28T12:05:33.753Z",    // 
        RetriedCount: 3,    // 
        MaxRetryCount: 1,    // 最大重试次数,范围:0-10
        DepartmentId: "2ec02a3b-07ff-4e77-a675-37e47dfa8c09",    // 部门id
        CompanyId: "67bed155-b70a-467d-869e-ccc84525aef6",    // 公司id
        CronTriggerId: "f6c0596c-e2b5-400f-b913-96b0d74c8d19",    // 
        JobExecutionPolicy: ,    // 
        CreatedAt: "2022-07-28T12:05:33.753Z",    // 创建时间
        CreatedBy: "6a1b2189-e895-4366-8b84-d49914038053",    // 创建人id
        CreatedByName: "this is a string",    // 创建人名称
        ModifiedAt: "2022-07-28T12:05:33.753Z",    // 更新时间
        ModifiedBy: "e1eb09f7-e6ca-411e-ac28-3a8c804e5033",    // 更新人id
        ModifiedByName: "this is a string",    // 更新人名称
        JobStartupType: ,    // 
        JobStartupId: "5a41c2d1-026c-4a76-8cd3-b1b332fffef8",    // 
        JobStartupName: "this is a string",    // 
        JobGroupId: "227e4c87-4043-41c1-ae96-35cff59f3650",    // 
        SeqNum: 90,    // 
        Step: "this is a string",    // 
        JobSubjectId: "34febe69-a2b5-445e-9d48-3954f4017a89",    // Job关联体id
        JobSubjectName: "this is a string",    // Job关联体名称
        JobSubjectType: "this is a string",    // Job关联体类型
      },
      {
        // 略，结构同前一节点
      }
    ]

#### 获取流程部署关联机器人列表

##### 功能介绍

根据指定流程部署Id，获取流程部署关联的机器人列表

##### 基本信息

**Path：** /openapi/workflows/{workflowId}/robots
**Method：** GET

**请求参考示例：**

无内容

**响应参考示例：**

    {
      Count: 68,    // 查询命中总记录数
      List: [
        {
          Id: "7886cf64-7e9b-4560-8d6e-b83686eb2619",    // 唯一标识
          CurrentDeviceId: "7ac22bfb-4b93-4047-b0d4-bf52211cd610",    // 当前所在设备的id
          LastHeartbeatTime: "2022-07-28T12:05:33.753Z",    // 最近心跳时间
          ListeningStatus: ,    // 许可状态
          CompanyId: "9cf225c9-3f4b-4547-8c4a-b2fcf2032dcc",    // 公司id
          DepartmentId: "8d3e870b-8668-4be2-9f86-15b5ee1bf231",    // 部门id
          SdkVersion: "this is a string",    // 机器人sdk版本
          Name: "this is a string",    // 名称
          Description: "this is a string",    // 备注
          CanUpgrade: false,    // 是否可升级
          LicenseStatus: "Unlicensed",    // 许可状态:Unlicensed-未许可;ClientLicensed-本地许可;ServerLicensed-控制台许可;PoolLicensed-浮动许可
          ClientSku: "this is a string",    // 机器人许可类型
          Version: "this is a string",    // 机器人版本
          Status: "Disconnected",    // 机器人状态:Disconnected-已断开;Ready-空闲;Busy-忙碌
        },
        {
          // 略，结构同前一节点
        }
      ],    // 当前页记录集合
    }

#### 批量增加流程部署关联机器人

##### 功能介绍

根据指定流程部署Id，为流程部署关联执行使用的机器人

##### 基本信息

**Path：** /openapi/workflows/{workflowId}/robots
**Method：** POST

**请求参考示例：**

    {
      RobotIds: ["9fecfb0b-ea2f-46a7-bf3d-b7ee3082dbdc","8f4799e2-0dae-4c4e-b837-5fee0d9988ad"],    // 机器人id列表
    }
**响应参考示例：**

    [
      {
        WorkflowId: "bb0b52fa-563d-4179-a235-65b2fb6cd6a8",    // 流程部署id
        RobotId: "d6f2f882-0020-4de5-b127-4f7fb2ce5ce1",    // 机器人Id
        ErrorMessage: "this is a string",    // 关联失败原因
      },
      {
        // 略，结构同前一节点
      }
    ]

#### 删除流程部署执行目标机器人

##### 功能介绍

根据指定流程部署Id，为流程部署移除关联的机器人

##### 基本信息

**Path：** /openapi/workflows/{workflowId}/robots/{robotId}
**Method：** DELETE

**请求参考示例：**

无内容

**响应参考示例：**

无内容

#### 批量更新（关联的）流程包到最新版本

##### 功能介绍

批量更新流程部署，使其使用最新版本流程包

##### 基本信息

**Path：** /openapi/workflows/batch/upgrade
**Method：** POST

**请求参考示例：**

    {
      WorkflowIds: ["3db63322-9d04-4ab7-aca6-b2e77f120b60","5c7cac2e-1ec1-4309-8707-8419de8efb2a"],    // 流程部署id列表
    }
**响应参考示例：**

    {
      Total: 20,    // 批量操作总数目
      SuccessCount: 18,    // 成功数量
      FailureCount: 1,    // 失败数量
      MissingCount: 1,    // 缺失（未找到记录）数量
      FailedList: [
        {
          Id: "this is a string",    // 唯一标识
          Name: "this is a string",    // 名称
          Message: "this is a string",    // 原因说明
        },
        {
          // 略，结构同前一节点
        }
      ],    // 失败详情
      WarningList: [
        {
          Id: "this is a string",    // 唯一标识
          Name: "this is a string",    // 名称
          Message: "this is a string",    // 原因说明
        },
        {
          // 略，结构同前一节点
        }
      ],    // 告警详情
    }

---

### 流程编排

#### 获取流程编排列表

##### 功能介绍

根据指定查询条件，获取流程编排列表

##### 基本信息

**Path：** /openapi/workflowlayouts
**Method：** GET
**Query string：**

 | 参数名称|参数类型|描述|
|:-|:-|:-|
|Name|String|名称，模糊匹配|
|SearchString|String|关键字，模糊匹配名称、备注、标签|
|IsDesc|Boolean|是否按创建时间降序排序，默认true,说明:true-降序；false-升序|
|PageIndex|Int|页码（从0开始）|
|PageSize|Int|页大小（默认20）|

**请求参考示例：**

无内容

**响应参考示例：**

    {
      Count: 73,    // 查询命中总记录数
      List: [
        {
          Id: "d78928fc-93ea-46ff-9e6f-797505e79fbd",    // 唯一标识
          Name: "this is a string",    // 名称
          Description: "this is a string",    // 备注
          Tags: ["this is a string","this is a string"],    // 标签
          Properties: {"Field1":"value1","Field2":"value2"},    // 附加属性
          Nodes: [
            {
              Id: "05c1b22b-763d-4214-a4db-311fcdf0a444",    // 唯一标识
              Index: 43,    // 索引
              Name: "this is a string",    // 名称
              WorkflowId: "30c02077-984a-4738-af97-58ab78b7b9eb",    // 流程部署id
              Description: "this is a string",    // 备注
              Tags: ["this is a string","this is a string"],    // 标签
              Arguments: [
                {
                  Name: "this is a string",    // 名称
                  Type: "this is a string",    // 参数类型
                  Direction: "In",    // 参数方向:In-入参;Out-出参;InOut-出入参
                  DefaultValue: "this is a string",    // 参数默认值; 当使用资产（即AssetContent不为null）时，这里为资产id
                  AssetContent: {
                    Name: "this is a string",    // 名称
                    Description: "this is a string",    // 备注
                    IsRequiredEncryption: false,    // 是否加密
                  },    // 使用的资产
                },
                {
                  // 略，结构同前一节点
                }
              ],    // 参数
              CreatedAt: "2022-07-28T12:05:33.754Z",    // 创建时间
              CreatedBy: "12502c09-1246-4385-a7c3-70dfe00403bf",    // 创建人id
              CreatedByName: "this is a string",    // 创建人名称
              ModifiedAt: "2022-07-28T12:05:33.754Z",    // 更新时间
              ModifiedBy: "3d59b588-9ad0-43d4-a29c-f41817f4a802",    // 更新人id
              ModifiedByName: "this is a string",    // 更新人名称
            },
            {
              // 略，结构同前一节点
            }
          ],    // 子任务列表
          LastTriggeredAt: "2022-07-28T12:05:33.754Z",    // 最后触发时间
          TriggeredCount: 11,    // 触发计数
          CompanyId: "19bfec47-91bd-45bf-ae4f-153fb9e8cf7f",    // 公司id
          DepartmentId: "baea6cdb-fa9c-4678-9056-30ed34e23924",    // 部门id
          CreatedAt: "2022-07-28T12:05:33.754Z",    // 创建时间
          CreatedBy: "27e6336c-455d-4dd3-ad3a-fa1195e47035",    // 创建人id
          CreatedByName: "this is a string",    // 创建人名称
          ModifiedAt: "2022-07-28T12:05:33.754Z",    // 更新时间
          ModifiedBy: "2d11d9de-53f5-40df-8057-3ee9d81d61db",    // 更新人id
          ModifiedByName: "this is a string",    // 更新人名称
        },
        {
          // 略，结构同前一节点
        }
      ],    // 当前页记录集合
    }

#### 根据id获取流程编排

##### 功能介绍

根据id获取流程编排详情

##### 基本信息

**Path：** /openapi/workflowlayouts/{workflowlayoutId}
**Method：** GET

**请求参考示例：**

无内容

**响应参考示例：**

    {
      Count: 69,    // 查询命中总记录数
      List: [
        {
          Id: "04da64c5-1558-491a-a1b2-f26513e04a76",    // 唯一标识
          Name: "this is a string",    // 名称
          Description: "this is a string",    // 备注
          Tags: ["this is a string","this is a string"],    // 标签
          Properties: {"Field1":"value1","Field2":"value2"},    // 附加属性
          Nodes: [
            {
              Id: "a9df636f-8f86-4eb7-bb5d-0741f6276343",    // 唯一标识
              Index: 64,    // 索引
              Name: "this is a string",    // 名称
              WorkflowId: "67ed3520-4952-42a4-b9a6-012cc42a5acc",    // 流程部署id
              Description: "this is a string",    // 备注
              Tags: ["this is a string","this is a string"],    // 标签
              Arguments: [
                {
                  Name: "this is a string",    // 名称
                  Type: "this is a string",    // 参数类型
                  Direction: "In",    // 参数方向:In-入参;Out-出参;InOut-出入参
                  DefaultValue: "this is a string",    // 参数默认值; 当使用资产（即AssetContent不为null）时，这里为资产id
                  AssetContent: {
                    Name: "this is a string",    // 名称
                    Description: "this is a string",    // 备注
                    IsRequiredEncryption: false,    // 是否加密
                  },    // 使用的资产
                },
                {
                  // 略，结构同前一节点
                }
              ],    // 参数
              CreatedAt: "2022-07-28T12:05:33.754Z",    // 创建时间
              CreatedBy: "631d66a2-7f65-43be-82d5-64ec8e7045aa",    // 创建人id
              CreatedByName: "this is a string",    // 创建人名称
              ModifiedAt: "2022-07-28T12:05:33.754Z",    // 更新时间
              ModifiedBy: "19475868-9824-477d-9152-e53a3b12aa8f",    // 更新人id
              ModifiedByName: "this is a string",    // 更新人名称
            },
            {
              // 略，结构同前一节点
            }
          ],    // 子任务列表
          LastTriggeredAt: "2022-07-28T12:05:33.754Z",    // 最后触发时间
          TriggeredCount: 56,    // 触发计数
          CompanyId: "11807f1d-7884-400e-b783-c4056e1d7617",    // 公司id
          DepartmentId: "6b82e61c-932d-4a81-9efd-9de4a251096c",    // 部门id
          CreatedAt: "2022-07-28T12:05:33.754Z",    // 创建时间
          CreatedBy: "a844e765-dc0f-49fb-bff6-7a8b9ba32f4f",    // 创建人id
          CreatedByName: "this is a string",    // 创建人名称
          ModifiedAt: "2022-07-28T12:05:33.754Z",    // 更新时间
          ModifiedBy: "10900ced-ca50-4d60-91d4-c484abe07ecf",    // 更新人id
          ModifiedByName: "this is a string",    // 更新人名称
        },
        {
          // 略，结构同前一节点
        }
      ],    // 当前页记录集合
    }

#### 执行流程编排

##### 功能介绍

根据id执行流程编排

##### 基本信息

**Path：** /openapi/workflowlayouts/{workflowlayoutId}/execute
**Method：** POST

**请求参考示例：**

无内容

**响应参考示例：**

    {
      Id: "5ee7c3ee-75ea-4e46-a899-803295ff7a79",    // 唯一标识
      Name: "this is a string",    // 名称
      StartedAt: "2022-07-28T12:05:33.755Z",    // 
      FinishedAt: "2022-07-28T12:05:33.755Z",    // 
      DepartmentId: "fec64d0c-433e-4674-90d3-5e0cb5afbb93",    // 部门id
      CompanyId: "db17b08f-a5cb-4a09-9d84-98695361727f",    // 公司id
      CreatedAt: "2022-07-28T12:05:33.755Z",    // 创建时间
      CreatedBy: "d3c6e68c-bd73-4eee-9108-fd21e29154c7",    // 创建人id
      CreatedByName: "this is a string",    // 创建人名称
      ModifiedAt: "2022-07-28T12:05:33.755Z",    // 更新时间
      ModifiedBy: "2333db5f-4afe-4e7b-be04-b6ad01155c5d",    // 更新人id
      ModifiedByName: "this is a string",    // 更新人名称
      JobStartupType: ,    // 
      JobStartupId: "339a2fd6-4f9e-4ddc-a778-8dc553580a2b",    // 
      JobStartupName: "this is a string",    // 
      OwnerId: "51a719a9-e381-4ea2-ad11-4cfc2d7fe41f",    // 
      Jobs: [
        {
          Id: "82d4f7fe-047f-4ab1-a3c6-8c0201f30000",    // 唯一标识
          WorkflowId: "b7c449f8-9f94-46b9-a3ab-ae86d7755917",    // 流程部署id
          Name: "this is a string",    // 名称
          Description: "this is a string",    // 备注
          PackageId: "498d3014-67bf-46f0-8f03-066cea7fe680",    // 流程包id
          PackageName: "this is a string",    // 流程包名称
          PackageVersion: "this is a string",    // 流程包版本号
          PackageVersionId: "b25e5604-22bf-42de-b03f-414ec90fafc5",    // 流程包版本id
          Arguments: [
            {
              Name: "this is a string",    // 名称
              Type: "this is a string",    // 参数类型
              Direction: "In",    // 参数方向:In-入参;Out-出参;InOut-出入参
              DefaultValue: "this is a string",    // 参数默认值; 当使用资产（即AssetContent不为null）时，这里为资产id
              AssetContent: {
                Name: "this is a string",    // 名称
                Description: "this is a string",    // 备注
                IsRequiredEncryption: false,    // 是否加密
              },    // 使用的资产
            },
            {
              // 略，结构同前一节点
            }
          ],    // 参数
          ContainingQueueId: "cbc47f95-ab13-414d-847f-b1b71d33bafc",    // 
          Priority: 2000,    // 优先级，范围:0-5000
          State: "Queued",    // :Queued-等待中;Running-运行中;Failed-失败;Cancelled-已取消;Succeeded-成功;Paused-已暂停
          DisplayState: "Queued",    // :Queued-等待中;Running-运行中;Failed-失败;Cancelled-已取消;Succeeded-成功;Paused-已暂停
          VideoRecordMode: "NeverRecord",    // 视频录制模式:NeverRecord-从不录制;ReportOnlyWhenSucceeded-仅成功时上传;ReportOnlyWhenFailed-仅失败时上传;AlwaysRecord-总是录制;AlwaysReport-总是上传
          Message: "this is a string",    // 原因说明
          MaxRunSeconds: 2,    // 
          Deleted: false,    // 
          LastRunInstaceId: "61d3ce27-fd53-43bf-b003-16ce5d36f46c",    // 
          LastRobotName: "this is a string",    // 
          StartedAt: "2022-07-28T12:05:33.755Z",    // 
          FinishedAt: "2022-07-28T12:05:33.755Z",    // 
          RetriedCount: 71,    // 
          MaxRetryCount: 1,    // 最大重试次数,范围:0-10
          DepartmentId: "fa152063-0cea-4b58-bb90-a3d24b642bf1",    // 部门id
          CompanyId: "815a575e-106d-44d6-bf9e-3149ee0dd76e",    // 公司id
          CronTriggerId: "236ae813-576f-4fb7-a68e-04592dc43d50",    // 
          JobExecutionPolicy: ,    // 
          CreatedAt: "2022-07-28T12:05:33.755Z",    // 创建时间
          CreatedBy: "0faf720f-82f2-464a-bb5a-74fee8b52d5a",    // 创建人id
          CreatedByName: "this is a string",    // 创建人名称
          ModifiedAt: "2022-07-28T12:05:33.755Z",    // 更新时间
          ModifiedBy: "e29f519b-2542-47ee-8311-b47895567573",    // 更新人id
          ModifiedByName: "this is a string",    // 更新人名称
          JobStartupType: ,    // 
          JobStartupId: "2d5f26f0-c7e1-4b62-8dd7-41f056b163f7",    // 
          JobStartupName: "this is a string",    // 
          JobGroupId: "7594fe54-a24a-450f-b410-c7a8ea1062e0",    // 
          SeqNum: 47,    // 
          Step: "this is a string",    // 
          JobSubjectId: "e30c0531-0f93-42a6-bcf8-fe04159712fb",    // Job关联体id
          JobSubjectName: "this is a string",    // Job关联体名称
          JobSubjectType: "this is a string",    // Job关联体类型
        },
        {
          // 略，结构同前一节点
        }
      ],    // 
    }

---
