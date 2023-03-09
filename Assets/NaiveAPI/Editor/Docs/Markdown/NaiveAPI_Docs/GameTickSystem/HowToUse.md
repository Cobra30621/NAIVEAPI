# HowToUse

## 1 Create Manager Object

1.1 Create a new gameObject

![image](img_0.png)


1.2 Add GameTickManager Script
![image](img_1.png)


1.3 Setting with inspector
![image](img_2.png)

--- 
## While Coding

1. Create New Script from Assets/Create/NaiveAPI/C# Tick Script
![image](img_3.png)
2. Setting update frequency

Change tickUpdate.Subscribe()'s param.
Default is Update TickRate.
![image](img_4.png)
![image](img_5.png)

--- 
## If you want to add ITickUpdate by yourself...

1. Add Interface “ITickUpdate”.
![image](img_6.png)
2. Coding in TickUpdate() (same as in Update()).
![image](img_7.png)
3. Subscribe to GameTickManager at Start().
    Do  not  invoke  this  in  Start()  if  you  can't ensure that
    this Awake is always slower than GameTick's Manager
![image](img_8.png)

--- 
## Things you have to know

Do not use(Input.GetKey(KeyCode.Mouse0))  it make many problem
Use this to replace it.
![image](img_9.png)
You can use tickUpdate.UnSubscribe() to stop update this script,
also can restart by use Subscribe() again.
