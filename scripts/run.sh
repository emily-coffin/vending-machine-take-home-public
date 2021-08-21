#!/bin/bash
set -eu

echo "Running App"
dotnet run --project VendingMachine -c Release --no-build