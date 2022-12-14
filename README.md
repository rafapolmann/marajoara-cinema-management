# Cine Management - Marajoara

This project presents a system for the management of cinema with the following functionalities:
* User register (manager, attendant and customer)
* Cine room register
* Movie register
* Session register
* In theater presentation
* Management and ticket order

Best practices like, DDD, layered architecture, SOLID, Unit Tests and Integration Tests, were followed during the development.

The purpose of this project was the study of it's used components and frameworks.

## 🛠️ Built With

### Back-end
* [Programming language C#](https://dotnet.microsoft.com/en-us/learn/csharp)
* [.NET 5.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)
* .Net Web Api
* FluentValidation
* MediatR
* AutoMapper
* FluentAssertions
* Moq
* Microsoft EntityFrameworkCore
* Swagger

### Database
* SQL Server LocalDB (for developing and Unit Tests)
* [SQL Server 2022 Docker Image](https://hub.docker.com/_/microsoft-mssql-server) (container deployment)

### Front-end

* [Angular](https://angular.io/start)
* [TypeScript](https://www.typescriptlang.org/)
* [Angular Material](https://material.angular.io/)
* [Nodejs 16.15.1](https://nodejs.org/)

### Deployment
* [Docker](https://docs.docker.com/)
* [Kubernetes](https://kubernetes.io/)

### Development tools
* [Visual Studio 2022 Community](https://visualstudio.microsoft.com/pt-br/vs/community/)
* [Visual Studio Code](https://code.visualstudio.com/)
* [Docker Desktop](https://www.docker.com/products/docker-desktop/)
* [KinD](https://kind.sigs.k8s.io/) - (k8s cluster on a local machine)


## 🚀 Starting

These instructions will permit you getting a copy of the project in your local machine for tests and developing. 

### 📋 Requirements

#### Editing the source code:
* You will need a code editor installed in your machine, such as **VS Code** or **Visual Studio**.
* You must have .Net SDK installed
* You must have NodeJs installed

#### Local deployment
* For building and running the application in Docker containers, you should have Docker installed. In our development environment, we used **Docker Desktop**.
* For deployment on Kubernetes in local machine, you could use **KinD**.


#### Obs.: All links for each setup could be found in **[Built With](https://github.com/rafapolmann/marajoara-cinema-management?readme=1#%EF%B8%8F-built-with)** section.


### ⌨️ Clone Git Repository

For cloning the repository in your local machine, you just create a folder, open a Terminal Command in this directory and execute the command below:


```
git clone https://github.com/rafapolmann/marajoara-cinema-management
```

#### Back-end editing
You should just open **Marajoara.Cinema.Management.sln** file with **Visual Studio**.

#### Front-end editing
You should just open **Marajoara.Cinema.Management.FrontEnd** directory with **VS Code**, open a new Terminal Command tab and execute the command below:

```
npm install
```

## ⚙️ Debugging execution

In this case the application will use SQL Server LocalDB.
* You must execute **Marajoara.Cinema.Management.Api** with **Visual Studio**.
* You need to change the **environment.ts** file in **Marajoara.Cinema.Management.FrontEnd** to use port: **44328** in baseApiUrl.
* Open **Marajoara.Cinema.Management.FrontEnd** directory with **VS Code**, open a new Terminal Command tab and execute the command below:

```
ng serve
```
After above steps:
* Access this address: http://localhost:4200/
* Access the Swagger in this address: https://localhost:44328/swagger/index.html

## 📦 Deployment

### Docker Compose

You must have Docker installed in your machine.

* Open a **Terminal Command** in **Marajoara.Cinema.Management** directory (where is **docker-compose.yml** file).
* You should create a folder in **c:/sqlvol/** or change the **docker-compose.yml** properties for a directory of your preference.
* Execute in **Terminal Command** the following command:

```
docker-compose up -d
```
After above steps:
* Access this address: http://localhost:4300/

### Kubernetes (Kind)

You must have Docker and KinD installed in your machine.

* To create a cluster, open a **Terminal Command** in **Marajoara.Cinema.Management\Kubernetes** (where are YAML configurations files)
* You should create a folder in **c:/kubedata** or change the **create-cluster.yaml** properties for a directory of your preference.
* Execute in **Terminal Command** the following commands:

```
kind create cluster --name {your-cluster-name} --config .\create-cluster.yaml
```
```
kubectl apply -f .\PersistentVolume.yaml
```
```
kubectl apply -f .\PersistentVolumeClaim.yaml
```
```
kubectl apply -f .\SqlServerDeployment.yaml
```
```
kubectl apply -f .\SqlServerService.yaml
```
```
kubectl apply -f .\MarajoaraBackendDeployment.yaml
```
```
kubectl apply -f .\MarajoaraBackendService.yaml
```
```
kubectl apply -f .\MarajoaraFrontendDeployment.yaml
```
```
kubectl apply -f .\MarajoaraFrontendService.yaml
```

After above steps:
* Access this address: http://localhost:4400/

#### Obs.: Both deployment (Docker and Kind) can't be up simultaneously on the same machine because back-end API use the same port: 44500.

### 🔩 First execution

On the first time running the application, there will be no user on the database. You should click in ***Registre-se aqui*** to create a user account.
OBS: The first user registered will have the Manager Access Level. The following will be registered as Customer

In order to create an account, you should inform an user name and e-mail. 

The default password will be created with pattern below:

* First part of e-mail.
* Concatenated with system default sufix: **P@ssW0rd**

```
Example:
User e-mail: newuser@somemail.com
Password: newuserP@ssW0rd
```

## ✒️ Authors

* [**Rafael Polmann**](https://github.com/rafapolmann) - *developer*
* [**Akauam Pitrez Westphal**](https://github.com/Akauam) - *developer*


## 📄 License

This project is licensed under the MIT License - see [LICENSE.md](https://github.com/rafapolmann/marajoara-cinema-management/blob/master/LICENSE.md) for details.
