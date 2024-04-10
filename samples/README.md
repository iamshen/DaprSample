

### Step 1: Run Service


```bash


cd Service

bash
```


```bash
dapr run --dapr-http-port 3500 --app-id my_actor_service --app-port 5001 --resources-path ../resources -- dotnet run
#dapr run --dapr-http-port 56001 --app-id my_actor_service --app-port 5001 --resources-path ../resources -- dotnet run
```


### Step 2: Run the client app


在 Dapr 中，客户端和服务端之间的通信是通过 Dapr 的 sidecar 进行的。Dapr 的 sidecar 默认监听 3500 端口来接收来自客户端的 HTTP 请求。如果在启动 Actor 服务时指定了一个不同于默认值（3500）的端口，那么需要告诉客户端应该通过哪个端口与 Dapr 的 sidecar 通信。

例如，如果您启动 Actor 服务时使用的命令是：

```
dapr run .... --dapr-http-port 56001
```

因此，您需要在运行客户端之前设置环境变量 DAPR_HTTP_PORT 为 56001 ，以便客户端知道通过哪个端口与 Dapr 的 sidecar 通信：

```bash

cd Client
bash

export DAPR_HTTP_PORT=56001

dapr run --app-id actorclient -- dotnet run
```