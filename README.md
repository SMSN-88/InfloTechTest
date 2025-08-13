# User Management Enhancements

This project builds upon an existing User Management application by adding new functionality, UI flows, and supporting tests.  
The work described here covers three key areas:

1. Filters Section

On the **Users** page, three filter buttons are available beneath the user listing:

- **Show All** (already implemented in the original project)
- **Active Only** – Shows only users whose `IsActive` property is `true`.
- **Non Active** – Shows only users whose `IsActive` property is `false`.

The filtering logic was implemented both in the service layer (`IUserService` / `UserService`) and the controller actions to ensure consistent behavior across the application.

---

2. User Model Properties

A new property was added to the `User` model:

```csharp```
public DateTime DateOfBirth { get; set; }

Purpose
Store and display each user's date of birth.

Used in the UI for both viewing and editing users.

Included in form validation to ensure correct date entry.

This property is surfaced in:

The Users listing

The Add/Edit user forms

The View user screen

---

3. Actions Section (CRUD Functionality)
The application now supports full CRUD operations for users:

Add – Form to create a new user. On success, redirects back to the list view.

View – Read-only page showing all details of a single user.

Edit – Form to update an existing user's details, with validation feedback.

Delete – Confirmation screen to remove a user from the system.

All actions:

Include validation for required fields and correct data types.

Provide clear success or error feedback to the end user.

Redirect appropriately on completion.

---

Unit Tests (Enhancements Only)
To support the above changes, additional unit tests were written extending the existing UserControllerTests suite.
The new tests verify:

Filters – That the "Active Only" and "Non Active" filters return the correct subsets of users.

DateOfBirth – That the DateOfBirth property is correctly mapped and preserved between the model, service, and views.

CRUD Actions – That Add, Edit, and Delete controller actions call the appropriate IUserService methods and return the expected view or redirect results.

These tests integrate with the existing mocking framework (Moq) and assertion library (FluentAssertions).



