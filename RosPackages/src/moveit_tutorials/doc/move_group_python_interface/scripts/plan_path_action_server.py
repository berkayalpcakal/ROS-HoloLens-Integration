#! /usr/bin/env python

import rospy
import actionlib
import moveit_tutorials.msg
import moveit_commander
import geometry_msgs.msg

class PlanPathActionServer(object):

    def __init__(self, name, move_group):
        self._feedback = moveit_tutorials.msg.PlanPathFeedback()
        self._result   = moveit_tutorials.msg.PlanPathResult()

        self._action_name = name
        self._move_group  = move_group
        self._as = actionlib.SimpleActionServer(self._action_name, moveit_tutorials.msg.PlanPathAction, execute_cb=self.execute_cb, auto_start = False)
        self._as.start()

        self.max_attemps = 20
    
    def execute_cb(self, goal):
        fraction = 0.0
        attemps = 0

        if(len(goal.waypoints)) > 0:
            self._feedback.log = 'Server starts with ' + str(len(goal.waypoints)) + ' waypoints.. \n'
            rospy.loginfo(self._feedback.log)
            self._as.publish_feedback(self._feedback)
            
            while(fraction < 1.0 and attemps < self.max_attemps):
                attemps += 1

                if self._as.is_preempt_requested():
                    rospy.loginfo('%s: Preempted' % self._action_name)
                    self._as.set_preempted()
                    break
                
                (plan, fraction) = self._move_group.compute_cartesian_path(
                                    goal.waypoints,   # waypoints to follow
                                    0.01,             # eef_step 1cm resolution
                                    0.0)              # jump_threshold

                log_str = 'Got fraction: ' + str(fraction) + ' after ' + str(attemps) + ' attemps'
                rospy.loginfo(log_str)
                self._feedback.log = log_str 
                self._as.publish_feedback(self._feedback)

            if fraction == 1.0:
                log_str = 'Path computed.'
                rospy.loginfo(log_str)
                self.plan = plan 
                self._feedback.log = log_str 
                self._as.publish_feedback(self._feedback)
                self._as.set_succeeded(self._result)    

            else:
                log_str = 'Path could not be computed. Aborting.'
                self._result.log = log_str
                self._as.set_aborted(self._result)         

        else:
            self._result.log = 'invalid number of waypoints'
