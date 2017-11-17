# Citrix StoreFront API sample build with .NET Core

This sample is a small web application, built on .NET Core, that shows
how to interact with the StoreFront WebAPI to build a small, functional application listing
and launching site. This is intended to demonstrate the APIs as well as provide a template
for you to if you would like to embed some of this functionality into your own applications/web sites (SharePoint, WordPress, etc).

## Prerequisites

Depending on which implementation you are interested in, your prereqs might be a little different. We have broken down the pre-reqs based on which path you would like.

- Infrastructure
    - Citrix XenApp or XenDesktop with a valid license.
    - Citrix StoreFront
- Docker
    - If you are using the docker image, make sure you have docker installed. We recommend Docker for Mac or Docker for Windows.
        - [Docker Mac](https://www.docker.com/docker-mac)
        - [Docker Windows](https://www.docker.com/docker-windows)
- Local install and debugging
  - .Net Core SDK Version 2 [Click Here](http://www.dot.net)
- A code editor. We like Visual Studio Code or Visual Studio IDE for the interactive debugging, but any editor will do. We like the following.
    - [Visual Studio Code](https://code.visualstudio.com/)
    - [Visual Studio Community IDE](https://www.visualstudio.com/free-developer-offers/)

## Getting Started

There are two ways to get started with this sample.

- Clone the repo and run it local so you can step through and debug to further understand the application.

    Below is the command to clone our repository.
    ``` sh
    git clone https://github.com/citrix/StorefrontSample-netcore.git
    ```
    Once you have cloned the repository, you can open the directory if your Visual Studio code and start debugging.

    You can also start the application up via the command line by executing the following commands in the cloned directory. Once those are execute you should be able to access the application by going to http://localhost:5000 in your browser.
    ``` sh
        dotnet clean
        dotnet restore
        dotnet build
        dotnet run
    ```

- Use the [Docker](http://www.docker.com) container that we have prebuilt for you. This contains .NET Core + the sample application if you would like an isolated environment to play around with.

    If you would like to get started with the docker implementation, follow these steps.

    - Check out the prerequisites for Docker.
    - Download the image from docker hub. [Here is the link](https://hub.docker.com/r/citrixdeveloper/citrix-storefront-apidemo/)
    - ```sh
        docker pull citrixdeveloper/citrix-storefront-apidemo
      ```
    - After the image has downloaded you will need to start the image by executing the following command. This will start the container web app and listen on port 5000.
    - ```sh
        docker run -d -p 5000:5000 citrixdeveloper/citrix-storefront-apidemo 
      ```

## Features for sample

- [ ] Provide better error handling
- [ ] Fix layout to it is more responsive
- [ ] Agregate applications from multiple sources.

## Built With

* [.NET Core (ASP.NET Core)](http://www.dot.net) - The application framework used
* [Visual Studio Code](http://code.visualstudio.com) - Code Editor
* [Docker](http://www.docker.com) - Used to build containers and testing.

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md)for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/your/project/tags). 

## Authors

* **John McBride** - *Initial work*
    - [Twitter](https://www.twitter.com/johnmcbride)
    - [Github](https://github.com/johnmcbride)
    - [Blog](https://blogs.citrix.com/author/johnmcb)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

