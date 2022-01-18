# Legion Society Contact App

## Description

This is a .NET Core 3.0 MVC test/playground application. This idea is based on a project I was assigned years ago, at a previous software shop.

A rotary club would like to create a private site where members can exchange contact information with each other.

- Each member will log in to the site and update basic demographic information (name, email, date of birth, etc.)
- Anonymous (unauthenticated users) are not permitted to see the list. Only authenticated users may view the contact list.
- Users may not update the demographic information for other users.
- To avoid spam/abusive users, new users must be invited by another user.
	- When an existing user logs in, the site will present a button to invite another user.
	- The site will send an email to the invited user, encouraging him to sign up.
	- When the invited user completes the form, a login will be created for the user.


## Notes

This project also serves to demonstrate some of the coding patterns I enjoy, such as the Repository pattern and Claims-based authorization.