
This is a source code to Rocket Elevators Commercial Controllers written in Csharp by Arman Adibi.

There is a test files included inside this repo that allows the customer to test and verify few scenarios.

The test files make sure that the Controller are working correctly and it is safe to be implented into our customers building.


### Installation

As long as you have **.NET 5.0** installed on your computer, nothing more needs to be installed:

The code to run the scenarios is included in the Commercial_Controller folder, and can be executed there with:

`dotnet run <SCENARIO-NUMBER>`

### Running the tests

To launch the tests, make sure to be at the root of the repository and run:

`dotnet test`
============== CONTENTS OF THIS Csharp SOURCE CODE ==============

 Battry.go

 * DECLARE ITS ATTRIBUTES (CONSTRUCTORS)
 * CREATE A NEW BATTERY ON EACH REQUEST(NewBattery Function)
 * CREATE AND HOLD THE COLUMNS
 * CREATE THE BUTTONS(REQUEST AND DESTINATION) FOR EACH COLUMNS
 * FIND THE RESPECTIVE COLUMN FOR EACH REQUEST ( BASEMENT IS SERVED BY COLUMN N'1)

    * COLUMN 1 : SERVE THE BASEMENT
    * THE RESTE OF THE COLUMNS SERVE THE DEVIDEN AMOUNT OF FLOOR BY THE RESTING COLUMNS

 * HANDLE THE FIRST REQUEST OF AN ELEVATOR BY FINDING THE BEST ELEVATOR INSIDE THE CHOSEN COLUMNS

 Column.go

 * DECLARE ITS ATTRIBUTES (CONSTRUCTORS)
 * CREATE ELEVATORS OBJECTS INSIDE EACH COLUMNS
 * FIND THE NEAREST ELEVATOR( MATH FUNCTION : FLOOR RQUESTED FROM - ACTUAL POSITION => WHICH ON IS CLOSEST TO 0)
 * SEND THE ELEVATOR TO IT DESTINATION FLOOR

ELEVATOR.go

 * HANDLE ELEVATOR ATTRIBUTES
 * MOVE THE ELEVATOR THROUGH THE QUEUE LIST UNTIL THERE IS NO MORE REQUEST
 * CONTAINS A COMPLETED LIST AND ADD EACH COMPLETED TRIP TO IT
 * OPERATES THE DOOR OF THE ELEVATOR
    
    * CHECK IF THERE IS AN OVERWEIGHT
    * CHECK IF THERE IS AN OBSTRUCTION

CALLBUTTON

 * HANDLE THE FIRST RQUEST BUTTONS ATTRIBUTES

FLOORRQUESTBUTTON:

 * HANDLE THE INSIDE  DESTINTION REQUEST BUTTONS ATTRIBUTES


DOORS:

 * HANDLE THE DOORS ATTRIBUTES


============== THE LOGIC OF THIS Csharp SOURCE CODE ==============

All the elevators are idle at the lobby which is the first floor. To request an elevator you must be at the lobby since there is no button inside the elevators. You have to chose your destination before entering the elevator. There is an amount of column where each column contains the amount of floor they will have to serve. So on each request, the user need to go the respective column and take the elevator that will be sent to him. To select an elevator, we go through each elevator in the chosen column and check which one is the closest to the floor that the request comes from. When user is picked up and reach it's destination, the elevator then make its way to the lobby again waiting for a new request.