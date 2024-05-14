# MSTest-appium-app-browserstack

This sample elaborates the [MSTest](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest) Integration with BrowserStack.

<img src="assets/browserstack.png" width=30 height=30> <img src="assets/MSTest.png" width=50 height=25> 

> To perform tests using SDK, please checkout the sdk branch

## Setup

### Installation Steps

1. Clone the repository.
2. Open the solution `mstest-appium-app-browserstack.sln` in Visual Studio.
3. Install dependencies using NuGet Package Manager:
    ```bash
    dotnet restore
    ```
4. Build the solution

### Adding Credentials

1. Add your BrowserStack Username and Access Key to the `Config/*.json` files in the project. 
   
    ```json
    {
        "userName": "BROWSERSTACK_USERNAME",
        "accessKey": "BROWSERSTACK_ACCESS_KEY",
    }
    ```

2. Alternatively, you can set environment variables given below. Note that the creds supplied in the config json files above, takes precedence over environment variables. In order to use env variables, remove the `userName` and `accessKey` from the config json files altogether.
    - `BROWSERSTACK_USERNAME`
    - `BROWSERSTACK_ACCESS_KEY`

You can get your browserstack credentials from [here](https://www.browserstack.com/accounts/profile/details)

### Uploading App

- Upload the app using the Browserstack [REST API](https://www.browserstack.com/docs/app-automate/appium/upload-app-from-filesystem).

- The response contains the app hashed id:
    ```json
    {
        "app_url": "bs://<app_hashed_id>"
    }
    ```
- Paste this app hashed id to the corresponding config file. For Example: The sample android app used for Android Single Test can be found [here](https://github.com/browserstack/mstest-appium-app-browserstack/blob/sdk/Android/WikipediaSample.apk). Upload the app and paste the hashed id [here](https://github.com/browserstack/mstest-appium-app-browserstack/blob/main/Android/Config/SingleTest.json#L5).

### Running Tests

- To run tests, execute the following command:
    ```bash
    dotnet test
    ```

- To run tests on Android/iOS, provide `<os>` to the below command. Where os can take either of two values: `Android` or `iOS`.
    ```bash
    dotnet test <os>
    ```

- To run the single test, execute the following command:
    ```bash
    dotnet test <os> --filter SingleTest
    ```

- To run tests in parallel, execute the following command:
    ```bash
    dotnet test <os> --filter ParallelTest
    ```

- To run local tests, execute the following command:
    ```bash
    dotnet test <os> --filter LocalTest
    ```

Understand how many parallel sessions you need by using our [Parallel Test Calculator](https://www.browserstack.com/app-automate/parallel-calculator?ref=github)

## Notes
* You can view your test results on the [BrowserStack automate dashboard](https://www.browserstack.com/app-automate)
* To test on a different set of devices or build a set of appium capabilities, use our [Capability Builder](https://www.browserstack.com/app-automate/capabilities?tag=w3c)
* You can export the environment variables for the Username and Access Key of your BrowserStack account

  * For Unix-like or Mac machines:
  ```
  export BROWSERSTACK_USERNAME=<browserstack-username> &&
  export BROWSERSTACK_ACCESS_KEY=<browserstack-access-key>
  ```

  * For Windows Cmd:
  ```
  set BROWSERSTACK_USERNAME=<browserstack-username>
  set BROWSERSTACK_ACCESS_KEY=<browserstack-access-key>
  ```

  * For Windows Powershell:
  ```
  $env:BROWSERSTACK_USERNAME=<browserstack-username>
  $env:BROWSERSTACK_ACCESS_KEY=<browserstack-access-key>
  ```

## Additional Resources
* [Documentation for writing automate playwright test scripts in C#](https://www.browserstack.com/docs/app-automate/appium/getting-started/c-sharp)
* [Capability Builder for App Automate](https://www.browserstack.com/app-automate/capabilities)
* [Real Mobile devices for Appium testing on BrowserStack](https://www.browserstack.com/list-of-browsers-and-platforms/app_automate)
* [Using REST API to access information about your tests via the command-line interface](https://www.browserstack.com/docs/app-automate/api-reference/introduction)

