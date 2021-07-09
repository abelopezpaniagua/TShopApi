pipeline {
  agent any
  
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
        bat "dotnet restore ${workspace}\\TShopApi\\TShopApi.sln"
      }
    }
  }
}

