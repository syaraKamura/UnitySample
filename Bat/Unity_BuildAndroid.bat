@echo off
SET UNITY_PATH=C:"\Program Files\Unity\Editor\Unity.exe"
SET UNITY_PROJECT_PATH="D:\New Unity Project 1\"
SET PASS=""
SET BUILD_TARGET=Android
SET METHOD=BuildClass.BuildFor

SET WAIT_FLAG=0

:LOOP

::Unity.exe�����s����Ă��邩�m�F����
TASKLIST | FIND "Unity.exe" > NUL
IF NOT ERRORLEVEL 1 (
	
	IF %WAIT_FLAG% == 0�@(
		ECHO Unity.exe�͋N�����Ă��܂��B
		ECHO �A�Z�b�g�o���h���̐������ł���܂ő҂��܂��B
		SET WAIT_FLAG=1
	)
	GOTO LOOP

) ELSE (
	ECHO �A�Z�b�g�o���h���̐����J�n
	GOTO EXEC
)


:EXEC

::Unity���N������
%UNITY_PATH% -batchmode -quit -password  %PASS% -projectPath %UNITY_PROJECT_PATH% -executeMethod %METHOD%%BUILD_TARGET% -logFile .\build.log
IF %errorlevel% == 1 (
echo �G���[���ɂȂ�܂���
)

IF %errorlevel% == 0 (
echo ����I��


)

EXIT /b