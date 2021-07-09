pipeline {
  agent winagent
  
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
  }
}

