# 内容清单
~~~
1、 DDD领域模型
2、 集成AutoFac、Log4net、MediatR、Jwt认证、Redis、MemoryCache
3、 引入ORM：EF、Dapper
4、 集成RabbitMQ，MongoDB
5、 集成Polly
6、 集成单元测试：Xunit、Mock
7、 加入VUE前端页面
8、 集成FluentValidation，自定义验证功能
9、 集成Redis哨兵（sentinel）模式、分片（cluster）模式
10、 集成Redis主从模式
11、 集成Jaeger分布式追踪组件
12、 集成Consul分布式服务注册和发现组件
13、 集成Ocelot分布式统一网关入口点
14、 集成IdentityServer4 验证
15、 集成Nginx+Ocelot+Consul
16、 集成CI/CD环境，Jenkins+Harbor+Natapp
17、 配置docker启动
18、 配置Docker-compose.yml，批量启动所需的组件（cwy516project+consul+nginx+redis）(todo:ocelot+mongodb+mysql)
19、 集成ElasticSearch
20、 集成Kafka消息队列
21、 集成Elasticsearch+Logstash+Kibana(ELK日志系统)(Filebeat+Kafka)
22、 集成Kafka集群
23、 集成RabbitMq集群（3.8+3.9，区别就是erlang.cookie的配置方式，最新的硬盘空间占用少）
24、 集成App.Metrics+InfluxDB+Grafana(todo)
25、 集成事件驱动：不同服务相互调用，只需要定义一个接口，其他接口通过事件响应的方式调用(todo)
26、 配置k8s集群
27、 集成gRPC（todo）
28、 集成apollo(阿波罗)配置中心（todo)
29、 集成Mongodb集群（todo）
30、 了解Socket通信（todo）
31、 集成CAP分布式事务框架（todo）

32、 源码解读（Swagger，MediatR，MongoDB，StackExchange.Redis，JWT，IdentityServer4）
33、 源码解读（aspnetcore 3.1 todo)
~~~


# 启动的准备工作
~~~
 1、配置mysql主从数据库（如果有的话），否则就只配置一个
 2、启动redis
 3、启动mongodb
 4、启动consul
 5、启动rabbitmq，如果有集群，通过docker-compose启动了容器之后，需要进入容器将节点加入集群（rabbitmqctl join_cluster --ram rabbit@rabbit1)
~~~
