# PicoPlacaPredictor

### Business Model
There are three basic models:
1. NoDriveDay. It contains which days are restricted.
2. NoDriveTime. It contains which range of hours are restricted. Start hour - End hour
3. DrivingRestriction. It defines dates and times are set as no-drive days for a plate number.

### Endpoint
This endpoint exposes one endpoint for verifying if a plate number can be on drive in particular date and time.   
. URL: ../api/driving/verify.   
. Request has the following input model.

- **plateNumber**. Number of plate to verify
- **date**. Input date (dd/MM/yyyy) E.G. 16/09/2019 
- **time**. Input time (HH:mm) E.G. 16:00   

```
{
"plateNumber" : "ABC-121",      
"date" : "18/09/2019",
"time" : "12:30"
}
```
. Response returns a string with further information.
```
The plate number 'ABC-121' CAN be on the road.

No-drive data for ABC-121:
No-Drive Date: 'MONDAY
No-Drive Time: 07:00-09:00 16:00-19:30
```

### Test instructions
- Hit the endpoint (GET) **../api/driving/verify**
 
