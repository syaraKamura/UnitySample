@echo off
SET UNITY_PATH=C:"\Program Files\Unity\Editor\Unity.exe"
SET UNITY_PROJECT_PATH="D:\New Unity Project 1\"
SET METHOD="AssetBundleWindow.BuildAssetundle"
SET ACOUNT="kamura.syara@gmail.com"
SET PASS=""

SET WAIT_FLAG=0

:LOOP

::Unity.exeが実行されているか確認する
TASKLIST | FIND "Unity.exe" > NUL
IF NOT ERRORLEVEL 1 (
	
	IF %WAIT_FLAG% == 0　(
		ECHO Unity.exeは起動しています。
		ECHO アセットバンドルの生成ができるまで待ちます。
		SET WAIT_FLAG=1
	)
	GOTO LOOP

) ELSE (
	ECHO アセットバンドルの生成開始
	GOTO EXEC
)



:EXEC

::Unityを起動する
%UNITY_PATH% -batchmode -quit -password  %PASS% -projectPath %UNITY_PROJECT_PATH% -executeMethod %METHOD% -logFile .\buildAssetBundle.log

IF %errorlevel% == 1 (
echo エラーになりました
)

IF %errorlevel% == 0 (
echo 正常終了
)

EXIT /b
