#! /usr/bin/env python

import rospy
import actionlib
import moveit_tutorials.msg
import moveit_commander
import geometry_msgs.msg

class ExecutePathActionServer(object):

    def __init__(self, name, move_group, plan_path_action_server):
        self._feedback = moveit_tutorials.msg.ExecutePathFeedback()
        self._result   = moveit_tutorials.msg.ExecutePathResult()

        self._action_name = name
        self._move_group  = move_group
        self._plan_path_action_server = plan_path_action_server
        self._as = actionlib.SimpleActionServer(self._action_name, moveit_tutorials.msg.ExecutePathAction, execute_cb=self.execute_cb, auto_start = False)
        self._as.start()

        self.max_attemps = 100
    
    def execute_cb(self, goal):
        if hasattr(self._plan_path_action_server, 'plan'):
        
            if(len(goal.waypoints)) > 0:
                self._feedback.log = 'Server starts with ' + str(len(goal.waypoints)) + ' waypoints.. \n'
                rospy.loginfo(self._feedback.log)
                self._as.publish_feedback(self._feedback)

                if self._as.is_preempt_requested():
                    rospy.loginfo('%s: Preempted' % self._action_name)
                    self._as.set_preempted()
                
                log_str = 'Moving the arm.'
                rospy.loginfo(log_str)
                self._feedback.log = log_str 
                self._as.publish_feedback(self._feedback)

                self._move_group.execute(self._plan_path_action_server.plan, wait=True)

                log_str = 'Execution completed.'
                rospy.loginfo(log_str)
                self._feedback.log = log_str 
                self._as.publish_feedback(self._feedback)                
                self._result.log = log_str
                self._as.set_succeeded(self._result)    
        
            else:
                log_str = 'invalid number of waypoints'
                rospy.loginfo(log_str)                
                self._result.log = log_str
                self._as.set_aborted(self._result)

        else:
            log_str = 'No plan was found, plan again!'
            rospy.loginfo(log_str)
            self._result.log = log_str
            self._as.set_aborted(self._result)          

