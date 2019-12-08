import subprocess
import os
from git import Repo

nuget = r'c:\ProgramData\chocolatey\lib\NuGet.CommandLine\tools\nuget.exe'
projects = ["armolib", "auxiliarylib", "datacommunicator", "HtmlDiff", "SandBox", "SatelSdk", "wcfextras", "zkveinsensor", "compassplayer" ]
projects = ["SatelSdk"]


def run_git(args):
    print(args)
    process = subprocess.Popen("git " + args, stdout=subprocess.PIPE)
    output = process.communicate()[0]
    if process.returncode != 0:
        sys.exit(process.errors)

def build_all():
    for proj in projects:
        script = "D:\\Projects\\" + proj + "\\Build\\build.py"
        if subprocess.call(['python.exe', script]) != 0:
            sys.exit(process.errors)


def nuget_run(self, command):
    print("Nuget " + command)
    if subprocess.call([nuget, command, self.solution_file]) != 0:
        sys.exit(process.errors)

def push_to_nuget_all():
    for proj in projects:
        temo_dir = "d:\\Temp\\" + proj + "_temp"
        for root, dirs, files in os.walk(temo_dir):
            for file in files:
                if  ".nupkg" in file:
                    full_dir = os.path.join(root, file)
                    print( "Push " + full_dir)
                    subprocess.call([nuget, 'push', full_dir, 'a4ab0201-5c18-41aa-b7a3-24216c5f44e0', '-src', 'http://devserver:81/nuget/ArmoProGet/'])

def pull_all():
    for project in projects:
        project_dir = "d:\\Projects\\" + project
        os.chdir(project_dir)
        repo = Repo(project_dir)
        print('Repository =' + project + ';Active branch = ' + repo.active_branch.name)
        run_git("pull --progress -v --no-rebase \"origin\" " + repo.active_branch.name)

def update_nuget_all():
    for proj in projects:
        solution = "D:\\Projects\\" + proj + "\\Source\\Main.sln"
        subprocess.call([nuget, 'update', solution])

def update_timex():
    subprocess.call([nuget, 'restore', 'd:\\Projects\\Timex\\Source\\Tests\\Tests.sln'])
    subprocess.call([nuget, 'restore', 'd:\\Projects\\Timex\\Source\\Others\\Others.sln'])
    subprocess.call([nuget, 'restore', 'd:\\Projects\\Timex\\Source\\Main\\Timex.sln'])
    
    subprocess.call([nuget, 'update', 'd:\\Projects\\Timex\\Source\\Tests\\Tests.sln'])
    subprocess.call([nuget, 'update', 'd:\\Projects\\Timex\\Source\\Others\\Others.sln'])
    subprocess.call([nuget, 'update', 'd:\\Projects\\Timex\\Source\\Main\\Timex.sln'])

#pull_all()
#build_all()
#update_nuget_all()
build_all()
push_to_nuget_all()

#update_timex()