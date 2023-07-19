using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ParticleEngine.Globals;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParticleEngine.Screens.TestScreens
{
    class TaskListTest : BaseScreen
    {
        private List<Worker> workers;
        private List<WorldObject> objectToMove;
        private List<ToDoTask> todoList;

        private int todoListTmr;

        public TaskListTest()
        {
            workers = new List<Worker>();
            objectToMove = new List<WorldObject>();
            todoList = new List<ToDoTask>();
            todoListTmr = 7500;

            for (int x = 0; x <= 9; x++)
            {
                workers.Add(new Worker());
            }
        }

        public override void Draw()
        {
            GlobalVars.SpriteBatch.Begin();

            GlobalVars.SpriteBatch.Draw(Textures.pixel, GlobalVars.GameSize, Color.DarkOliveGreen);

            foreach (Worker _wrk in workers)
            {
                GlobalVars.SpriteBatch.Draw(Textures.pixel, new Rectangle((int)_wrk.position.X, (int)_wrk.position.Y, _wrk.size.X, _wrk.size.Y), Color.Orange);
                GlobalVars.SpriteBatch.Draw(Textures.pixel, new Rectangle((int)_wrk.targetPosition.X, (int)_wrk.targetPosition.Y, _wrk.size.X, _wrk.size.Y), Color.Orange * .25f);
                GlobalVars.SpriteBatch.DrawString(Fonts.Calibri,
                    "Taskstate: " + _wrk.TaskState.ToString() + Environment.NewLine +
                    "Pos: " + _wrk.position.ToString() + Environment.NewLine +
                    "Distance: " + _wrk.distanceBetweenTargetPos.ToString() + Environment.NewLine +
                    "Speed: " + _wrk.speed + Environment.NewLine +
                    "TargetSpeed: " + _wrk.targetSpeed,
                    _wrk.position, Color.White);
            }

            foreach (WorldObject _wo in objectToMove)
            {
                GlobalVars.SpriteBatch.Draw(Textures.pixel, new Rectangle((int)_wo.position.X, (int)_wo.position.Y, _wo.size.X, _wo.size.Y), _wo.colour);
            }

            foreach (ToDoTask _tasks in todoList)
            {
                GlobalVars.SpriteBatch.Draw(Textures.pixel, new Rectangle((int)_tasks.TargetPosition.X, (int)_tasks.TargetPosition.Y, _tasks.ObjectToBeMoved.size.X, _tasks.ObjectToBeMoved.size.Y), Color.Blue * .25f);
            }

            for (int x = 0; x <= todoList.Count - 1; x++)
            {
                string _tskWork = "Task " + x + ". In Progress: No. Finished: No. Priority: " + todoList[x].taskPriority;

                if (todoList[x].AssignedWorker != null)
                {
                    foreach (Worker _wrk in workers)
                    {
                        if (todoList[x].AssignedWorker == _wrk)
                        {
                            _tskWork = "Task " + x + ". In Progress: Yes. Finished: No. Priority: " + todoList[x].taskPriority;
                            break;
                        }
                    }
                }

                if (todoList[x].finished)
                {
                    _tskWork = "Task " + x + ". In Progress: Yes. Finished: Yes. Priority: " + todoList[x].taskPriority;
                }

                GlobalVars.SpriteBatch.DrawString(Fonts.Calibri, _tskWork, new Vector2(0, 15 + x * (15)), Color.White);


            }

            GlobalVars.SpriteBatch.DrawString(Fonts.Calibri, "Tasks: " + todoList.Count.ToString(), Vector2.Zero, Color.White);


            GlobalVars.SpriteBatch.End();
        }

        public override void Update()
        {

            foreach (Worker _wrk in workers)
            {
                _wrk.Update();
            }

            if (todoListTmr < 0)
            {
                todoListTmr = 150;

                foreach (ToDoTask _tasks in todoList)
                {
                    if (_tasks.AssignedWorker == null)
                    {
                        foreach (Worker _wrk in workers)
                        {
                            if (_wrk.TaskState == WorkerTaskStates.WaitingForTask)
                            {
                                _wrk.currentTask = _tasks;
                                _tasks.AssignedWorker = _wrk;
                                todoListTmr = 250;
                                return;
                            }
                        }

                    }

                    if (_tasks.finished)
                    {
                        todoList.Remove(_tasks);
                        todoListTmr = 75;
                        break;
                    }
                }
            }
            else
            {
                todoListTmr -= (int)GlobalVars.GameTime.ElapsedGameTime.TotalMilliseconds;
            }
        }

        public override void HandleInput()
        {
            if (UserInput.LeftMouseClick())
            {
                WorldObject _object = new WorldObject(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
                objectToMove.Add(_object);
            }

            if (UserInput.RightMouseClick())
            {
                if (objectToMove.Count != 0)
                {
                    ToDoTask _newTask = new ToDoTask(objectToMove[objectToMove.Count - 1], new Vector2(Mouse.GetState().X, Mouse.GetState().Y), 50);

                    todoList.Add(_newTask);
                }
            }

            if (UserInput.KeyPressed(Keys.Space))
            {
                foreach (Worker _wrk in workers)
                {
                    if (_wrk.TaskState == WorkerTaskStates.WaitingForTask)
                    {
                        _wrk.TaskState = WorkerTaskStates.NotAcceptingTasks;
                    }
                    else if (_wrk.TaskState == WorkerTaskStates.NotAcceptingTasks)
                    {
                        _wrk.TaskState = WorkerTaskStates.WaitingForTask;
                    }
                }
            }

            if (UserInput.KeyPressed(Keys.D1))
            {
                todoList.Clear();
                objectToMove.Clear();

                WorldObject _newObject;

                ToDoTask _newTask;

                for (int x = 0; x <= 99; x++)
                {
                    _newObject = new WorldObject(new Vector2(0 + (x * 32), GlobalVars.GameSize.Height - 32));
                    _newTask = new ToDoTask(_newObject, new Vector2(GlobalVars.GlobalRandom.Next(0, GlobalVars.GameSize.Width), GlobalVars.GlobalRandom.Next(0, GlobalVars.GameSize.Height)), GlobalVars.GlobalRandom.Next(1, 101));

                    objectToMove.Add(_newObject);
                    todoList.Add(_newTask);

                }

                todoList.Sort((toDo1, toDo2) => toDo1.taskPriority.CompareTo(toDo2.taskPriority));

            }

        }

    }

    class WorldObject
    {
        public Vector2 position;
        public Point size;
        public Color colour;

        public WorldObject(Vector2 _pos)
        {
            position = _pos;
            size = new Point(32, 32);
            colour = Color.Blue;
        }
    }

    class Worker
    {
        public Vector2 position;
        public Point size;
        public Vector2 targetPosition;
        public ToDoTask currentTask;
        public WorkerTaskStates TaskState;
        public Vector2 HomePoint;

        public float speed;
        public float distanceBetweenTargetPos;
        public float targetSpeed;

        public Worker()
        {
            position = new Vector2(GlobalVars.GameSize.Width / 2, Globals.GlobalVars.GameSize.Height / 2);
            size = new Point(32, 32);
            targetPosition = position;
            currentTask = null;
            TaskState = WorkerTaskStates.WaitingForTask;
            speed = 0.5f;
            targetSpeed = speed;
            distanceBetweenTargetPos = 0f;
            HomePoint = position;
        }

        public void Update()
        {

            distanceBetweenTargetPos = Vector2.Distance(position, targetPosition);

            if (currentTask == null)
            {
                if (distanceBetweenTargetPos < 1f)
                {

                    if (Vector2.Distance(HomePoint, position) < 500)
                    {
                        targetPosition = new Vector2(
                            new Random().Next((int)position.X - 64, (int)position.X + 64),
                            new Random().Next((int)position.Y - 64, (int)position.Y + 64));
                    }
                    else
                    {
                        targetPosition = HomePoint;
                    }

                }
                else
                {
                    MoveToTargetPos();
                }

            }
            else
            {

                switch (TaskState)
                {
                    case WorkerTaskStates.NotAcceptingTasks:

                        currentTask.AssignedWorker = null;
                        currentTask = null;

                        break;

                    case WorkerTaskStates.WaitingForTask:

                        targetPosition = currentTask.ObjectToBeMoved.position;
                        TaskState = WorkerTaskStates.MovingToObject;
                        break;

                    case WorkerTaskStates.MovingToObject:

                        if (distanceBetweenTargetPos < 1f)
                        {
                            TaskState = WorkerTaskStates.MovingObjectToDestination;
                            targetPosition = currentTask.TargetPosition;
                        }
                        else
                        {
                            MoveToTargetPos();
                        }

                        break;
                    case WorkerTaskStates.MovingObjectToDestination:

                        if (distanceBetweenTargetPos < 1f)
                        {
                            currentTask.ObjectToBeMoved.position = currentTask.TargetPosition;
                            TaskState = WorkerTaskStates.Finished;
                        }
                        else
                        {
                            MoveToTargetPos();
                            currentTask.ObjectToBeMoved.position.X = position.X - 5;
                            currentTask.ObjectToBeMoved.position.Y = position.Y - 5;
                        }

                        break;
                    case WorkerTaskStates.Finished:
                        currentTask.finished = true;
                        currentTask = null;
                        TaskState = WorkerTaskStates.WaitingForTask;
                        //targetPosition = new Vector2(GlobalVars.GameSize.Width / 2, Globals.GlobalVars.GameSize.Height / 2);
                        targetPosition = new Vector2(position.X + new Random().Next(-32, 32), position.Y + new Random().Next(-32, 32));

                        break;
                }

            }

        }

        private void MoveToTargetPos()
        {
            if (targetPosition.X > position.X)
            {
                position.X += speed;
            }

            if (targetPosition.X < position.X)
            {
                position.X -= speed;
            }

            if (targetPosition.Y > position.Y)
            {
                position.Y += speed;
            }

            if (targetPosition.Y < position.Y)
            {
                position.Y -= speed;
            }

            targetSpeed = distanceBetweenTargetPos / 50;

            if (targetSpeed > 8.5f)
            {
                targetSpeed = 8.5f;
            }
            else if (targetSpeed < 0.5f)
            {
                targetSpeed = 0.5f;
            }

            if (speed < targetSpeed)
            {
                speed += 0.1f;

                if (speed > targetSpeed) speed = targetSpeed;

            }
            else if (speed > targetSpeed)
            {
                speed -= 0.1f;

                if (speed < targetSpeed) speed = targetSpeed;
            }


            //if (position.X % 1 != 0) position.X -= 0.5f;
            //if (position.Y % 1 != 0) position.Y -= 0.5f;
        }
    }

    enum WorkerTaskStates
    {
        WaitingForTask, MovingToObject, MovingObjectToDestination, Finished, NotAcceptingTasks
    }

    class ToDoTask
    {
        public Worker AssignedWorker;
        public WorldObject ObjectToBeMoved;

        public Vector2 TargetPosition;
        public bool finished;
        public int taskPriority; // out of 100

        public ToDoTask(WorldObject _object, Vector2 _newPos, int _priority)
        {
            AssignedWorker = null;
            ObjectToBeMoved = _object;
            TargetPosition = _newPos;
            finished = false;
            taskPriority = _priority;
        }

    }


}
