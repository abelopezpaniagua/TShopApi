pipeline {
  agent { label 'winagent' }
  
  stages {
    stage ('Clean workspace') {
      steps {
        echo 'Clean Workspace..'
        cleanWs()
      }
    }
    stage('Git Checkout') {
      steps {
        git branch: 'master', url: 'https://github.com/abelopezpaniagua/TShopApi.git'
      }
    }
    stage('Restore packages') {
      steps {
        bat "dotnet restore ${workspace}\\TShopApi.sln"
      }
    }
    stage('Clean Solution')
    {
      steps {
        bat "dotnet clean ${workspace}\\TShopApi.sln" 
      }
    }
    stage('Build') {
      steps {
        bat "dotnet build --no-restore"
      }
    }
  }
}

