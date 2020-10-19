@REM ontolis-merge-plugin launcher script
@REM
@REM Environment:
@REM JAVA_HOME - location of a JDK home dir (optional if java on path)
@REM CFG_OPTS  - JVM options (optional)
@REM Configuration:
@REM ONTOLIS_MERGE_PLUGIN_config.txt found in the ONTOLIS_MERGE_PLUGIN_HOME.
@setlocal enabledelayedexpansion

@echo off

if "%ONTOLIS_MERGE_PLUGIN_HOME%"=="" set "ONTOLIS_MERGE_PLUGIN_HOME=%~dp0\\.."

set "APP_LIB_DIR=%ONTOLIS_MERGE_PLUGIN_HOME%\lib\"

rem Detect if we were double clicked, although theoretically A user could
rem manually run cmd /c
for %%x in (!cmdcmdline!) do if %%~x==/c set DOUBLECLICKED=1

rem FIRST we load the config file of extra options.
set "CFG_FILE=%ONTOLIS_MERGE_PLUGIN_HOME%\ONTOLIS_MERGE_PLUGIN_config.txt"
set CFG_OPTS=
if exist "%CFG_FILE%" (
  FOR /F "tokens=* eol=# usebackq delims=" %%i IN ("%CFG_FILE%") DO (
    set DO_NOT_REUSE_ME=%%i
    rem ZOMG (Part #2) WE use !! here to delay the expansion of
    rem CFG_OPTS, otherwise it remains "" for this loop.
    set CFG_OPTS=!CFG_OPTS! !DO_NOT_REUSE_ME!
  )
)

rem We use the value of the JAVACMD environment variable if defined
set _JAVACMD=%JAVACMD%

if "%_JAVACMD%"=="" (
  if not "%JAVA_HOME%"=="" (
    if exist "%JAVA_HOME%\bin\java.exe" set "_JAVACMD=%JAVA_HOME%\bin\java.exe"
  )
)

if "%_JAVACMD%"=="" set _JAVACMD=java

rem Detect if this java is ok to use.
for /F %%j in ('"%_JAVACMD%" -version  2^>^&1') do (
  if %%~j==java set JAVAINSTALLED=1
  if %%~j==openjdk set JAVAINSTALLED=1
)

rem BAT has no logical or, so we do it OLD SCHOOL! Oppan Redmond Style
set JAVAOK=true
if not defined JAVAINSTALLED set JAVAOK=false

if "%JAVAOK%"=="false" (
  echo.
  echo A Java JDK is not installed or can't be found.
  if not "%JAVA_HOME%"=="" (
    echo JAVA_HOME = "%JAVA_HOME%"
  )
  echo.
  echo Please go to
  echo   http://www.oracle.com/technetwork/java/javase/downloads/index.html
  echo and download a valid Java JDK and install before running ontolis-merge-plugin.
  echo.
  echo If you think this message is in error, please check
  echo your environment variables to see if "java.exe" and "javac.exe" are
  echo available via JAVA_HOME or PATH.
  echo.
  if defined DOUBLECLICKED pause
  exit /B 1
)


rem We use the value of the JAVA_OPTS environment variable if defined, rather than the config.
set _JAVA_OPTS=%JAVA_OPTS%
if "!_JAVA_OPTS!"=="" set _JAVA_OPTS=!CFG_OPTS!

rem We keep in _JAVA_PARAMS all -J-prefixed and -D-prefixed arguments
rem "-J" is stripped, "-D" is left as is, and everything is appended to JAVA_OPTS
set _JAVA_PARAMS=
set _APP_ARGS=

:param_loop
call set _PARAM1=%%1
set "_TEST_PARAM=%~1"

if ["!_PARAM1!"]==[""] goto param_afterloop


rem ignore arguments that do not start with '-'
if "%_TEST_PARAM:~0,1%"=="-" goto param_java_check
set _APP_ARGS=!_APP_ARGS! !_PARAM1!
shift
goto param_loop

:param_java_check
if "!_TEST_PARAM:~0,2!"=="-J" (
  rem strip -J prefix
  set _JAVA_PARAMS=!_JAVA_PARAMS! !_TEST_PARAM:~2!
  shift
  goto param_loop
)

if "!_TEST_PARAM:~0,2!"=="-D" (
  rem test if this was double-quoted property "-Dprop=42"
  for /F "delims== tokens=1,*" %%G in ("!_TEST_PARAM!") DO (
    if not ["%%H"] == [""] (
      set _JAVA_PARAMS=!_JAVA_PARAMS! !_PARAM1!
    ) else if [%2] neq [] (
      rem it was a normal property: -Dprop=42 or -Drop="42"
      call set _PARAM1=%%1=%%2
      set _JAVA_PARAMS=!_JAVA_PARAMS! !_PARAM1!
      shift
    )
  )
) else (
  if "!_TEST_PARAM!"=="-main" (
    call set CUSTOM_MAIN_CLASS=%%2
    shift
  ) else (
    set _APP_ARGS=!_APP_ARGS! !_PARAM1!
  )
)
shift
goto param_loop
:param_afterloop

set _JAVA_OPTS=!_JAVA_OPTS! !_JAVA_PARAMS!
:run
 
set "APP_CLASSPATH=%APP_LIB_DIR%\ru.knova.ontolis-merge-plugin-1.0.jar;%APP_LIB_DIR%\org.scala-lang.scala-library-2.11.8.jar;%APP_LIB_DIR%\net.sourceforge.owlapi.owlapi-apibinding-4.2.5.jar;%APP_LIB_DIR%\net.sourceforge.owlapi.owlapi-api-4.2.5.jar;%APP_LIB_DIR%\org.tukaani.xz-1.5.jar;%APP_LIB_DIR%\net.sf.trove4j.trove4j-3.0.3.jar;%APP_LIB_DIR%\com.google.guava.guava-18.0.jar;%APP_LIB_DIR%\com.google.inject.guice-4.0.jar;%APP_LIB_DIR%\javax.inject.javax.inject-1.jar;%APP_LIB_DIR%\aopalliance.aopalliance-1.0.jar;%APP_LIB_DIR%\com.google.inject.extensions.guice-assistedinject-4.0.jar;%APP_LIB_DIR%\com.google.inject.extensions.guice-multibindings-4.0.jar;%APP_LIB_DIR%\com.google.code.findbugs.jsr305-2.0.1.jar;%APP_LIB_DIR%\commons-io.commons-io-2.4.jar;%APP_LIB_DIR%\net.sourceforge.owlapi.owlapi-impl-4.2.5.jar;%APP_LIB_DIR%\net.sourceforge.owlapi.owlapi-parsers-4.2.5.jar;%APP_LIB_DIR%\net.sourceforge.owlapi.owlapi-oboformat-4.2.5.jar;%APP_LIB_DIR%\net.sourceforge.owlapi.owlapi-tools-4.2.5.jar;%APP_LIB_DIR%\net.sourceforge.owlapi.owlapi-fixers-4.2.5.jar;%APP_LIB_DIR%\net.sourceforge.owlapi.owlapi-rio-4.2.5.jar;%APP_LIB_DIR%\org.openrdf.sesame.sesame-model-2.7.16.jar;%APP_LIB_DIR%\org.openrdf.sesame.sesame-util-2.7.16.jar;%APP_LIB_DIR%\org.openrdf.sesame.sesame-rio-api-2.7.16.jar;%APP_LIB_DIR%\org.openrdf.sesame.sesame-rio-languages-2.7.16.jar;%APP_LIB_DIR%\org.openrdf.sesame.sesame-rio-datatypes-2.7.16.jar;%APP_LIB_DIR%\org.openrdf.sesame.sesame-rio-binary-2.7.16.jar;%APP_LIB_DIR%\org.openrdf.sesame.sesame-rio-n3-2.7.16.jar;%APP_LIB_DIR%\org.openrdf.sesame.sesame-rio-turtle-2.7.16.jar;%APP_LIB_DIR%\org.openrdf.sesame.sesame-rio-nquads-2.7.16.jar;%APP_LIB_DIR%\org.openrdf.sesame.sesame-rio-ntriples-2.7.16.jar;%APP_LIB_DIR%\org.openrdf.sesame.sesame-rio-rdfjson-2.7.16.jar;%APP_LIB_DIR%\org.openrdf.sesame.sesame-rio-rdfxml-2.7.16.jar;%APP_LIB_DIR%\org.openrdf.sesame.sesame-rio-trix-2.7.16.jar;%APP_LIB_DIR%\org.openrdf.sesame.sesame-rio-trig-2.7.16.jar;%APP_LIB_DIR%\com.github.jsonld-java.jsonld-java-sesame-0.5.0.jar;%APP_LIB_DIR%\com.github.jsonld-java.jsonld-java-0.5.0.jar;%APP_LIB_DIR%\com.fasterxml.jackson.core.jackson-core-2.3.3.jar;%APP_LIB_DIR%\com.fasterxml.jackson.core.jackson-databind-2.3.3.jar;%APP_LIB_DIR%\com.fasterxml.jackson.core.jackson-annotations-2.3.0.jar;%APP_LIB_DIR%\org.apache.httpcomponents.httpclient-cache-4.2.5.jar;%APP_LIB_DIR%\org.apache.httpcomponents.httpclient-4.2.5.jar;%APP_LIB_DIR%\org.apache.httpcomponents.httpcore-4.2.4.jar;%APP_LIB_DIR%\commons-codec.commons-codec-1.6.jar;%APP_LIB_DIR%\org.semarglproject.semargl-sesame-0.6.1.jar;%APP_LIB_DIR%\org.semarglproject.semargl-core-0.6.1.jar;%APP_LIB_DIR%\org.semarglproject.semargl-rdfa-0.6.1.jar;%APP_LIB_DIR%\org.semarglproject.semargl-rdf-0.6.1.jar;%APP_LIB_DIR%\org.slf4j.jcl-over-slf4j-1.7.7.jar;%APP_LIB_DIR%\org.slf4j.slf4j-simple-1.7.13.jar;%APP_LIB_DIR%\org.slf4j.slf4j-api-1.7.13.jar"
set "APP_MAIN_CLASS=ru.knova.ontolis.merge.MergeMaster"

if defined CUSTOM_MAIN_CLASS (
    set MAIN_CLASS=!CUSTOM_MAIN_CLASS!
) else (
    set MAIN_CLASS=!APP_MAIN_CLASS!
)

rem Call the application and pass all arguments unchanged.
"%_JAVACMD%" !_JAVA_OPTS! !ONTOLIS_MERGE_PLUGIN_OPTS! -cp "%APP_CLASSPATH%" %MAIN_CLASS% !_APP_ARGS!

@endlocal


:end

exit /B %ERRORLEVEL%
