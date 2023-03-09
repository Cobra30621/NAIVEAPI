# Notice

## Why GetKey make problem in GameTickSystem

![image](img_0.png)
GetKeyDown() & GetKeyUp() change value in only one frame,
And GameTickManager doesn’t update with Unity’s Update(),
Than it will return the wrong value. (Actually is the value we don’t want)

--- 
![image](img_1.png)
To solve this problem, I extended its signal to match the different update frequency.
( Also the custom frequency, that’s most annoying )

--- 
So when you need to use Input.GetKey() use this instead.
![image](img_2.png)

