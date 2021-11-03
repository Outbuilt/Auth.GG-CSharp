# Auth.GG Licensing for .NET

This API serves as a layer that simplifies communication between .NET and the server

> You can access the documentation of the API at https://setup.auth.gg

# Getting started

### Grab API Secret

* **Step 1** : Login to your panel and create your application
* **Step 2** : Your application secret will be next to your application name
* **Step 3** : Copy your secret and store it somewhere
### Grab AID
* **Step 1** : Login to your panel hover over your avatar on the top right
* **Step 2** : Click on ``Account Settings``
* **Step 3** : Copy your AID and store it somewhere

### Connecting panel to program
Now that you have your ``AID`` and ``Secret`` use it to initialize and connect your application to our servers
```
 OnProgramStart.Initialize("APPNAME", "AID", "PROGRAMSECRET", "VERSION");
```
## Example
```
 OnProgramStart.Initialize("MyDemoApp", "269868", "t5d7rzzbrdAHmfWTGmuTUazjLIvWk", "1.0");
```
> After a successful initialization, the server will send back the following information on your application based on the settings you have picked
* ``ApplicationSettings.Name`` : Application name
* ``ApplicationSettings.Status`` : Application Enabled/Disabled
* ``ApplicationSettings.DeveloperMode`` : DeveloperMode Enabled/Disabled
* ``ApplicationSettings.Hash`` : Applications md5 hash to check integrity
* ``ApplicationSettings.Version`` : Applications version
* ``ApplicationSettings.UpdateLink`` : Applications link that it updates from if version is updated
* ``ApplicationSettings.Freemode`` : Freemode Enabled/Disabled
* ``ApplicationSettings.Login`` : Login Enabled/Disabled
* ``ApplicationSettings.Register`` : Login Enabled/Disabled
*  ``ApplicationSettings.TotalUsers`` : Total users signed up for application
## Login

```
 if (API.Login(username, password))
                    {
                    //Code you want to do here on successful login
MessageBox.Show("You have successfully logged in!", OnProgramStart.Name, MessageBoxButton.OK, MessageBoxImage.Information);
                    }
```
> After a successful login, the server will send back the following information on your user
* ``User.Username`` : Users username 
* ``User.ID`` : Users ID
* ``User.Email`` : Users email
* ``User.HWID`` : Users hardware ID
* ``User.IP`` : Users IP
* ``User.UserVariable`` : Users variable
* ``User.Rank`` : Users rank
* ``User.Expiry`` : Users expiry
* ``User.LastLogin`` : Users last login
* ``User.RegisterDate`` : Users registration date
* ``User.ProfilePicture`` : Users profile picture link
## Register

```
 if (API.Register(username, password, email, license))
                    {
                    //Code you want to do here on successful register
MessageBox.Show("You have successfully registered!", OnProgramStart.Name, MessageBoxButton.OK, MessageBoxImage.Information);
                    }
```
## Extend Subscription
```
 if (API.ExtendSubscription(username, password, token))
                {
                    MessageBox.Show("You have successfully extended your subscription!", OnProgramStart.Name, MessageBoxButton.OK, MessageBoxImage.Information);
                    // Do code of what you want after successful extend here!
                }
```
## All in one - strictly key based - login & register with one
```
   if(API.AIO(KEY))
                {
                    //Code you want to do here on successful login
                    MessageBox.Show("Welcome back to my application!", OnProgramStart.Name, MessageBoxButton.OK, MessageBoxImage.Information);
                    Process.GetCurrentProcess().Kill(); // closes the application
                }
                else
                {
                    //Code you want to do here on failed login
                    MessageBox.Show("Your key does not exist!", OnProgramStart.Name, MessageBoxButton.OK, MessageBoxImage.Error);
                    Process.GetCurrentProcess().Kill(); // closes the application
                }
```
## Log Action

```
 API.Log("USERNAMEHERE", "ACTION HERE");
```
## Update Profile Picture

```
 API.UploadPic("USERNAMEHERE", @"C:\imagelocation.png");
```
