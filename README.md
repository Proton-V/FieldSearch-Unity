# FieldSearch-Unity V1.0.0
 
 ![image](https://user-images.githubusercontent.com/65833201/188505738-bd94910c-7cef-46c2-bad5-632436056de6.png)
 
- Add to project without git handler
- Ability to set SearchableEditor for all MonoBehaviour
- Add to project without git handler
- Save your requests between sessions with caching logic
- Default Search with criteria (ObjName, FieldName) && options (StartWith, IgnoreCase)
- Create your own search logic if necessary

### Tested on:
 - Unity Editor: 2018.4.3f1 | 2020.3.35f1
 - OS: Windows 10

# Quick start
## Step 1 (Optional)

You can install this as package (nit: git will always handle "package.json" && lock file).

Use **PackageManager -> Add package from git URL...**
>https://github.com/Proton-V/FieldSearch-Unity.git?path=/FieldSearch/Assets/FieldSearch

If you want example of using SearchableEditor for specific MonoBehaviour please see **Step 2**

## Step 2

***Skip this if you installed this as package && you don't want to add a "FieldSearchDemo" folder***

Download && Import [latest asset package V1.0.0](https://github.com/Proton-V/FieldSearch-Unity/releases/download/V1.0.0/FieldSearch-UnityV1.0.0.unitypackage)

**"FieldSearchDemo" folder (optional) contains example of using SearchableEditor for specific MonoBehaviour**
- [ ] Uncheck "FieldSearchDemo" folder **if you don't need it**.
- [ ] Uncheck "FieldSearch" folder **if you have this as pacakge**.

![image](https://user-images.githubusercontent.com/65833201/188316669-6702911b-3fbb-4670-8ae4-c4234a7d4cd5.png)

## Step 3

Create instance of default settings
**Field Search -> Add default settings ...**

![image](https://user-images.githubusercontent.com/65833201/188503443-b4f4a59a-9d86-495f-b24c-63015428a75b.png)

## Step 4 (Optional)

**Field Search -> Add package folders to .gitignore (global)**

After adding this package as asset you have tracked files for you git repo.

***You can remove it for git without updating .gitignore in repo***

Before press:

![image](https://user-images.githubusercontent.com/65833201/188505094-ca7a51d4-0a4d-405e-815c-9ec218d6d68d.png)

After press:

![image](https://user-images.githubusercontent.com/65833201/188505211-4babd641-af57-469b-8758-acd4ed020d9d.png)

### ***Also you can undo it***
**Field Search -> Remove package folders from .gitignore (global)**

# Settings
![image](https://user-images.githubusercontent.com/65833201/188504195-966a86db-de92-498c-955a-1b513e7e212c.png)
1. Apply SearchableEditor to all MonoBehaviour (who don't use custom inspector)
2. You can create && set custom SearchableEditorConfigObject to change default view of SearchableEditor
3. Save cache to disk to use previous cache between sessions
4. Memory limit in MB - memory and disk cache limit
5. Clear cache buttons
