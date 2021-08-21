#!/bin/bash
set -eu

echo "Run Tests"
dotnet test -c Release --no-build