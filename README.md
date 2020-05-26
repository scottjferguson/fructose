<img src="https://github.com/relay-dev/fructose/raw/master/resources/icon.png?raw=true" alt="Fructose" height="200" width="200">

# Fructose

[![Build status](https://ci.appveyor.com/api/projects/status/8v9lmg4x0di78yl9/branch/master?svg=true)](https://ci.appveyor.com/project/sfergusonATX/fructose/branch/master)
[![NuGet Release](https://img.shields.io/nuget/v/relay.core.plugins.svg)](https://www.nuget.org/packages/Relay.Core.Plugins/)
[![MyGet Release](https://img.shields.io/myget/relay-dev/v/Relay.Core.Plugins.svg)](https://www.myget.org/feed/relay-dev/package/nuget/Relay.Core.Plugins)
[![License](https://img.shields.io/github/license/relay-dev/core-plugins.svg)](https://github.com/relay-dev/core-plugins/blob/master/LICENSE)

A Customer management microservice platform

> <sup>Fructose is a set of microservices that facilitates common customer management tasks. The microservices expose a RESTful API that requires token-based header authentication. Each domain also supports handling events raised within the platform.</sup>
>
> <sup>Fructose is built on ASP.NET Core 2.2 and WebAPI. It is deployed to Azure using Docker.
>
> <sup>Communication amongst microservices is achieved using a generic abstraction of an Event Bus. There are 2 implementations; RabbitMQ and Azure Service Bus. They can be toggled by way of configuration.</sup>