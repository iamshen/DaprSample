# 使用 .Net Aspire CLI 部署微服务

## 准备环境

- 安装 Aspir8 
	- `dotnet tool install -g aspirate --prerelease`
- 配置本地 Docker 注册表
	- `这个用例里面使用了阿里云的容器镜像服务（ https://www.aliyun.com/product/acr）`
	- `当然也可以是本地 registry， 运行下面的脚本使用本地 registry (参考：https://www.docker.com/blog/how-to-use-your-own-registry-2/)`
	- `docker run -d -p 5001:5000 --restart always --name registry registry:2`

## Quick Start

```
# 初始化配置： 可以跳过这步，因为我运行过了，（可以删除掉，aspirate.json aspirate-state.json 再重新 init）
aspirate init 
# 构建项目并推送到 Registry 仓库
aspirate build
# 默认生成 kubernetes yaml 清单 (如果上一步已经构建过了，可以添加参数 --skip-build 跳过构建)
aspirate generate
# 也可以生成 docker-compose 部署，然后使用 docker compose up -d 
# aspirate generate --output-format compose
aspirate apply
```



## 1. 生成清单文件：导航到“应用主机”项目并执行：

```bash
cd .\build\DaprTool.Solution.AppHost\
dotnet run --publisher manifest --output-path manifest.json
```


## 2. 初始化 Aspir 8

```bash
aspirate init
```


## 3. 使用 Aspir8 通过执行以下命令构建项

```bash
aspirate build
```


## 4. 生成 部署 清单


请选择下面的 k8s 或者 docker compose 方式都可以, 根据具体环境选择


### 4.1 k8s

```bash
aspirate generate
```

### 4.2 docker compose

```bash
aspirate generate --output-format compose
```



## 5. 运行以下命令进行部署

```bash
# 这一步就是应用生成的清单文件了，如果本地有k8s 可以直接运行，否则手动执行 docker compose 也可。
aspirate apply
```




# 参考文献

- Aspir8 -  
- https://prom3theu5.github.io/aspirational-manifests/getting-started.html

