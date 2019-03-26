# Mssql Backup Scheduler
**MS-SQL Backup Scheduler**

## What Is It?
+ A .Net console application which backups and purges MS-SQL databases with Task Scheduler 
+ SSMS Express edition does NOT include Maintenance Tool which can back up databases automatically. This small tool is for poor guys like me who can not afford to buy commercial SSMS edition

## What It Can Do?
+ Backup database automatically
+ Purge old database automatically
 
## How Can Use It?
+ Adjust some variables at your needs
    + ConnectionString : Connection String of your DB Server
    + BackupFolder : Folder where all .bak files will be saved
    + ValidDay : Days for .bak files can be exist. all .bak files which is older than this value will be deleted
+ Register MssqlBackupScheduler.exe to Task Scheduler
