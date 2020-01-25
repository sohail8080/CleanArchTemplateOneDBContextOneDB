﻿using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using CleanArchTemplate.Common.UOW;
using CleanArchTemplate.BC.AccessControl.Account.Domain.Models;

namespace CleanArchTemplate
{
    public partial class Startup
    {
        // Configuring the Auth and Auth of Asp.net Identity.
        // For more information on configuring authentication, 
        //please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            // every request has its own instance.
            // Like DI Container Configuration
            // In asp.ent coare we have explicty internal DI Container.

            // Configuare ApplicationDbContext: how it will be servred per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);

            // Configure ApplicationUserManager, how it will be server per context in app
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Configure ApplicationSignInManager, how it will be served per request in app
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the 
            //  signed in user, sessionid is stored in cookie.
            // and to use a cookie to temporarily store information about a user 
            // logging in with a third party login provider
            // Configure the sign in cookie
            // APPLICATION LOGIN INFO IS STORED IN TEH COOKIE
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                // VEYR IMPORTATN, THIS TELL WHAT CONTROLLER ACTION WILL RETURN
                // THE LOGIN PAGE.
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp 
                    // when the user logs in.
                    // This is a security feature which is used when you change a password or 
                    // add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });


            // Configure the app to use owin middleware based cookie authentication for external
            // identities
            // EXT LOGIN INFO IS STORED IN THE COOKIE
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);


            // Enables the application to temporarily (5 Minutes) store user information when they are verifying 
            //the second factor in the two-factor authentication process.
            //     Configures a cookie intended to be used to store the partial credentials for
            //     two factor authentication
            // PARTIAL CREDENTIALS ARE STORED FOR 5 MINTUES
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be 
            // remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            //     Configures a cookie intended to be used to store whether two factor authentication
            //     has been done already
            // REMEMBER THAT 2FA HAS BEEN DONE IN THE CLIENT BROWSER COOKIE
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
    }
}