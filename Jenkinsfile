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
        git branch: 'master', credentialsId: 'a53803ef-81b3-4e31-b493-1d59ba7eb410', url: 'https://github.com/abelopezpaniagua/TShopApi.git'
      }
    }
    stage('Restore packages') {
      steps {
        bat "dotnet restore ${workspace}\\TShopApi\\TShopApi.sln"
      }
    }
  }
}

