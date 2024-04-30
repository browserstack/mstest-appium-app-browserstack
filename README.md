# MSTest-appium-app-browserstack

This sample elaborates the [MSTest](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest) Integration with BrowserStack.

<img src="assets/browserstack.png" width=30 height=30> <img src="assets/MSTest.png" width=50 height=25> 

Note: SDK Support for MSTest Framework only works on Windows.

## Setup

### Installation Steps

1. Clone the repository.
2. Open the solution `mstest-appium-app-browserstack.sln` in Visual Studio.
3. Install dependencies using NuGet Package Manager:
    ```bash
    dotnet restore
    ```
4. Build the solution

### Setup

1. Add your BrowserStack Username and Access Key to the `browserstack.yml` files in the Android/iOS project. You can get your browserstack credentials from [here](https://www.browserstack.com/accounts/profile/details)
    ```yml
    userName: BROWSERSTACK_USERNAME
    accessKey: BROWSERSTACK_ACCESS_KEY
    ```

2. Provide the path to your apk/ipa file in `browserstack.yml`.
    ```yml
    app: ./WikipediaSample.apk
    ```

3. To run your test on multiple devices, update the devices list in `browserstack.yml`.
    ```yml
    platforms:
        - deviceName: Samsung Galaxy S22 Ultra
            osVersion: 12.0
            platformName: android
    ```

4. To run your tests using Browserstack Local. Update the `browserstack.yml` with the following key:
    ```yml
    browserstackLocal: true # <boolean> (Default false)
    ```

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

- To run local tests, execute the following command:
    ```bash
    dotnet test <os> --filter LocalTest
    ```

Understand how many parallel sessions you need by using our [Parallel Test Calculator](https://www.browserstack.com/app-automate/parallel-calculator?ref=github)

## Notes
* You can view your test results on the [BrowserStack automate dashboard](https://www.browserstack.com/app-automate)
* To test on a different set of devices or build a set of appium capabilities, use our [Capability Builder](https://www.browserstack.com/app-automate/capabilities?tag=w3c)

## Additional Resources
* [Documentation for writing automate playwright test scripts in C#](https://www.browserstack.com/docs/app-automate/appium/getting-started/c-sharp)
* [Capability Builder for App Automate](https://www.browserstack.com/app-automate/capabilities)
* [Real Mobile devices for Appium testing on BrowserStack](https://www.browserstack.com/list-of-browsers-and-platforms/app_automate)
* [Using REST API to access information about your tests via the command-line interface](https://www.browserstack.com/docs/app-automate/api-reference/introduction)

