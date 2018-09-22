# BankLedger
A Bank Ledger console app created for a software job application.
Here is the specification from which I worked:
```
You have been tasked with writing the world’s greatest banking ledger. 
Please code a solution that can perform the following workflows through 
a console application (accessed via the command line):

-Create a new account
-Login
-Record a deposit
-Record a withdrawal
-Check balance
-See transaction history
-Log out
```

The solution includes a driver class called "BankLedger" which creates and maintains a dictionary of "Account" objects accessed by username, as well as the Account class.


Included as well is a set of functional tests for the Account class, called AccountTests.

`NOTE: THIS IS NOT A SECURITY DEMO IN ANY WAY`

No serious attempt at encryption of passwords has been made, as that stands a serious chance of eclipsing the rest of the project in scope and complexity by itself. While the ability of the driver to load an account for use with its intended accession methods is password restricted, no objects are obfuscated or encrypted in any way and passwords are stored in plain text.

## Usage - main menu
Upon launching the main executable, the console window will display the main menu:
```
Welcome to Chrisbank!
Enter 1 to create new account.
Enter 2 to Log in to an account.
Enter 9 to Exit
```
Input is awaited from the command line; valid choices will display further prompts and menus, invalid options will be called out as such (and navigation will not move), and entering 9 will exit up one level (exit the whole program from the main menu, or logout from the account menu).

### Account creation
Minimal validation is done for usernames and passwords (they cannot be blank), and additional stipulations could be implemented easily.
Accounts are created with no transaction history and a balance of $0.00.

### Logging in to an account
After entering an existing username, the user gets 4 total attempts to get the password right, prompted again if they enter an incorrect one. Running out of attempts aborts the login attempt and returns to main menu, and additional consequences could be easily implemented at the relevant step.
After supplying a correct password for the chosen username, a user sees the account menu, detailed below.

### Exiting
Choosing this option will terminate the program.

## Usage - account menu
After logging into an account, the account menu for the current user is displayed:
```
Welcome user <Username Will be Displayed Here>.
Enter 1 to check balance.
Enter 2 to record a deposit.
Enter 3 to record a withdrawal.
Enter 4 to view transaction history.
Enter 9 to logout
```

### Checking balance
Selecting this option will display the user's current balance. Fractional pennies are accepted for deposit and withdrawal, but displayed numbers will be rounded to the nearest cent. The C# decimal type is used for all numbers for its increased precision and well-defined behavior for financial applications.
Afterwards, the user is immediately returned to the account menu.

### Recording deposit
Selecting this option will prompt the user for a positive decimal quantity without currency symbol. Amounts at or below 0.00 will be rejected. 
Upon entering a valid amount, the user will be prompted for confirmation; entering exactly the character 'y' will confirm the deposit, anything else will abort.
Upon confirmation, the entered amount will be subtracted from the account's balance, an entry will be created in the transaction history, and the user will be returned to the account menu.


### Recording withdrawal
Selecting this option will first check the user's balance. If it is at or below $0.00, the user will be notified they can't withdraw without funds and will be returned to the account menu.
Otherwise, it will prompt the user for a positive decimal quantity without currency symbol. Amounts at or below 0.00 will be rejected.
Upon entering a valid amount, the user will be prompted for confirmation; entering exactly the character 'y' will confirm the withdrawal, anything else will abort.
Of note, an account can become overdrawn if it began with a positive balance; further withdrawals will be halted, but the withdrawal causing the overdraft stands and the balance can become negative.
Upon confirmation, the entered amount will be subtracted from the account's balance, an entry will be created in the transaction history, and the user will be returned to the account menu.

### Viewing transaction history
Selecting this option will display a tabular list of transactions in this account, in chronological order with timestamp, amount, and transaction type. To aid in tallying, in this view deposits are positive and withdrawals are negative.
If no transactions have occurred, there will be a notification to this effect.

### Logging out
Selecting this option will return the user to the main menu

#### Known Issues
There is one undesirable behavior with current currency formatting - transaction history will display deposits and withdrawals of less than $0.05 as $0.00, but their aggregate could affect the account balance and leave a hypothetical user confused how their history lines up with their balance total. This could be fixed with further formatting tinkering or any number of custom types explicitly designed for currency, but I've decided to keep it as 
