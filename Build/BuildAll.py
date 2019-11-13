import subprocess
import os

nuget = r'c:\ProgramData\chocolatey\lib\NuGet.CommandLine\tools\nuget.exe'
projects = ["armolib", "auxiliarylib", "datacommunicator", "HtmlDiff", "SandBox", "SatelSdk", "wcfextras", "zkveinsensor", "compassplayer" ]
#projects = ["SatelSdk"]

def build_all():
    for proj in projects:
        script = "D:\\Projects\\" + proj + "\\Build\\build.py"
        subprocess.call(['python.exe', script])

def nuget_run(self, command):
    print("Nuget " + command)
    p = subprocess.call([nuget, command, self.solution_file])
    return p!=1

def push_all():
    for proj in projects:
        temo_dir = "d:\\Temp\\" + proj + "_temp"
        for root, dirs, files in os.walk(temo_dir):
            for file in files:
                if  ".nupkg" in file:
                    full_dir = os.path.join(root, file)
                    print( "Push " + full_dir)
                    subprocess.call([nuget, 'push', full_dir, 'a4ab0201-5c18-41aa-b7a3-24216c5f44e0', '-src', 'http://devserver:81/nuget/ArmoProGet/'])

def update_nuget_all():
    for proj in projects:
        solution = "D:\\Projects\\" + proj + "\\Source\\Main.sln"
        subprocess.call([nuget, 'update', solution])

build_all()
update_nuget_all()
build_all()
#push_all()