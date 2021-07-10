pipeline {
  agent { label 'winagent' }
  
  stages {
    stage ('Clean workspace') {
      steps {
        echo 'Clean Workspace..'
        cleanWs()
      }
    }
    stage('Build') {
      steps {
          echo 'Building..'
      }
    }
    stage('Git Checkout') {
      steps {
        git branch: 'master', credentialsId: '', url: 'https://github.com/abelopezpaniagua/TShopApi.git'
      }
    }
    stage('Restore packages') {
      steps {
        bat "dotnet clean ${workspace}\\TShopApi.sln --configuration Release && dotnet nuget locals all --clear" 
        bat "dotnet nuget add source https://api.nuget.org/v3/index.json -n nuget.org --configfile $env:APPDATA\\NuGet\\NuGet.Config"
        bat "dotnet restore ${workspace}\\TShopApi.sln"
      }
    }
  }
}

