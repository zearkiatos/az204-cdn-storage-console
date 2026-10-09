@echo off
REM Console application runner script for Windows
REM This script provides commands to build and run the Redis Cache Console

setlocal enabledelayedexpansion

if "%1"=="run" (
    dotnet run --project RedisCacheConsole.csproj
    goto end
)

if "%1"=="local" (
    dotnet run --project RedisCacheConsole.csproj
    goto end
)

if "%1"=="test" (
    call run.bat docker-test-up
    timeout /t 5
    ENVIRONMENT=test dotnet test RedisCacheConsole.slnx
    call run.bat docker-test-down
    rd /s /q data-test
    goto end
)

if "%1"=="test-coverage" (
        call run.bat docker-test-up
        timeout /t 5
    	ENVIRONMENT=test dotnet test tests/RedisCacheConsole.Tests/RedisCacheConsole.Tests.csproj \
        /p:CollectCoverage=true \
        /p:CoverletOutputFormat=cobertura \
        /p:CoverletOutput=./TestResults/ \
        /p:Threshold=60 \
        /p:ThresholdType=line \
        /p:ThresholdStat=total \
        /p:Exclude="[xunit.*]*"
        call run.bat docker-test-down
        rd /s /q data-test
    goto end
)

if "%1"=="test-coverage-report" (
        call run.bat docker-test-up
        timeout /t 5
        dotnet reportgenerator -reports:"tests/RedisCacheConsole.Tests/TestResults/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
        timeout /t 1
        start coveragereport/index.html
        call run.bat docker-test-down
        rd /s /q data-test
    goto end
)

if "%1"=="build" (
    dotnet build RedisCacheConsole.csproj
    goto end
)

if "%1"=="docker-up" (
    docker-compose up
    goto end
)

if "%1"=="docker-down" (
    docker-compose down
    goto end
)

if "%1"=="podman-up" (
    podman-compose up
    goto end
)

if "%1"=="podman-down" (
    podman-compose down
    goto end
)

if "%1"=="docker-local-up" (
    docker-compose -f docker-compose.local.yaml up
    goto end
)

if "%1"=="docker-local-down" (
    docker-compose -f docker-compose.local.yaml down
    goto end
)

if "%1"=="podman-local-up" (
    podman-compose -f docker-compose.local.yaml up
    goto end
)

if "%1"=="podman-local-down" (
    podman-compose -f docker-compose.local.yaml down
    goto end
)

if "%1"=="docker-test-up" (
    docker-compose -f docker-compose.test.yaml up -d --wait
    goto end
)

if "%1"=="docker-test-down" (
    docker-compose -f docker-compose.test.yaml down
    goto end
)

if "%1"=="podman-test-up" (
    podman-compose -f docker-compose.test.yaml up -d --wait
    goto end
)

if "%1"=="podman-test-down" (
    podman-compose -f docker-compose.test.yaml down
    goto end
)

if "%1"=="" (
    echo Usage: run.bat [command]
    echo.
    echo Commands:
    echo   run       - Build and run the console application
    echo   build     - Build the console application only
    echo   docker-local-up   - Start the local Docker environment
    echo   docker-local-down - Stop the local Docker environment
    echo   podman-local-up   - Start the local Podman environment
    echo   podman-local-down - Stop the local Podman environment
    echo   docker-up   - Start the Docker environment
    echo   docker-down - Stop the Docker environment
    echo   podman-up   - Start the Podman environment
    echo   podman-down - Stop the Podman environment
    echo   local     - Build and run the console application locally
    echo   test      - Run the unit tests
    echo   test-coverage - Run the unit tests with code coverage
    echo   test-coverage-report - Generate the test coverage report
    echo   docker-test-up   - Start the test Docker environment
    echo   docker-test-down - Stop the test Docker environment
    echo   podman-test-up   - Start the test Podman environment
    echo   podman-test-down - Stop the test Podman environment
    goto end
)

echo Unknown command: %1
echo Use 'run.bat' with no arguments to see available commands.

:end
endlocal
