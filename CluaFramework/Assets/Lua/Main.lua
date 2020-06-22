local GameObject = UnityEngine


Loger("我是lua")

function main(x,y)
	Loger("it's main")
	UnityEngine.create("opop") --创建一个空物体
	--Loger("go 返回的信息 "..go)

	--Loger("XXXXXXXXXXX "..UnityEngine.name)
	--local go = UnityEngine.Find("App")
	return x+y

end

function Start()
	Loger("it's start")
end

function Update()
	--Loger("it's Update")

end

function FixedUpdate()
	--Loger("it's FixedUpdate")
end

function OnApplicationQuit()
	Loger("it's OnApplicationQuit")
end
