
XCOPY WebBase\static\logo.png Resources\mipmap-hdpi\Icon.png* /Y
XCOPY WebBase\static\logo.png Resources\mipmap-mdpi\Icon.png* /Y
XCOPY WebBase\static\logo.png Resources\mipmap-xhdpi\Icon.png* /Y
XCOPY WebBase\static\logo.png Resources\mipmap-xxhdpi\Icon.png* /Y
XCOPY WebBase\static\logo.png Resources\mipmap-xxxhdpi\Icon.png* /Y

rmdir /S /Q Assets\static 
XCOPY WebBase\dist\* Assets\ /s /i /Y
pause
pause