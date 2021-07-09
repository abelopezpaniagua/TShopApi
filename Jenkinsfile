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
        git branch: 'master', credentialsId: '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', url: 'https://github.com/abelopezpaniagua/TShopApi.git'
      }
    }
    stage('Restore packages') {
      steps {
        bat "dotnet restore ${workspace}\\TShopApi\\TShopApi.sln"
      }
    }
  }
}

