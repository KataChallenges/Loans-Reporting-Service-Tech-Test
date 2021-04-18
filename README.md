# Introduction 
Auden Tech Test to Design Solution for the below problem:

## Problem
You have been asked to analyse a dataset of loans and come up with an aggregate view of the data.
Specifically, we want to help the CEO with the following problem:

```
AS the CEO
I WANT to know how many loans we have issued grouped by £100
SO THAT I can understand what size of loans we should focus on
```

# Build and Test
To build the application you need to use visual studio with latest .net core 5 and C# 9 installed. Select IIS Express option and run the solutoin. Use swagger to test the endpoint with two different data source types (i.e. textfile, database). 
Application has been designed in .net 5 as an api. Which based on the choise of datasource will return the data back to the user in below format:

[
  {
    "amount": "£101",
    "totalRecords": 30
  },
  {
    "amount": "£102",
    "totalRecords": 33
  }
]




# Other Material
Some screen shoots have been added to the main directory under "screenshoots" folder. 